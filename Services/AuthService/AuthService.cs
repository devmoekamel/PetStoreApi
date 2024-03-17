using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PetStore.DTOS;
using PetStore.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using PetStore.Helper;
using System.Security.Claims;
namespace PetStore.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JWT _jwt;

        public AuthService(UserManager<ApplicationUser> userManager,IOptions<JWT> jwt)
        {
            this._userManager = userManager;
            this._jwt = jwt.Value;
        }
        
        public async Task<AuthDTO> RegisterAsync(RegisterDTO registerDTO)
        {
            if (await _userManager.FindByNameAsync(registerDTO.UserName) is not null)
                return new AuthDTO { Message = "UserName already Exist" };
            if (await _userManager.FindByEmailAsync(registerDTO.Email) is not null)
                return new AuthDTO { Message = "Email already Exist" };

            var newUser = new ApplicationUser
            {
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                Email = registerDTO.Email,
                PasswordHash = registerDTO.Password,
                UserName = registerDTO.UserName,
            };
         var result = await _userManager.CreateAsync(newUser,registerDTO.Password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach(var error in result.Errors)
                    errors += $"{error.Description}  \n";
               return new AuthDTO { Message = errors }; 
            }
            await _userManager.AddToRoleAsync(newUser, "User");

            var jwtToken = await CreateJwtToken(newUser);

            return new AuthDTO
            {
                Email = newUser.Email,
                ExpiredOn = jwtToken.ValidTo,
                IsAuthenticated = true,
                Id = newUser.Id,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                UserName = newUser.UserName,
            };
        }
        public async Task<AuthDTO> LoginAsync(LoginDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return new AuthDTO { Message = "Email Or Password is Wrong" };



            var authmodel = new AuthDTO();

            var JwtSecurityToken = await CreateJwtToken(user);
            var Roles = await _userManager.GetRolesAsync(user);



            return new AuthDTO
            {
                ExpiredOn = JwtSecurityToken.ValidTo,
                Email = user.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken),
                UserName = user.UserName,
                Roles = Roles.ToList(),
                IsAuthenticated = true,
                Id = user.Id

            };
        }
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
           var SymmetricSecurityKey =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
           var SigningCredentials = new SigningCredentials(SymmetricSecurityKey,SecurityAlgorithms.HmacSha256);

           var userClaims = await _userManager.GetClaimsAsync(user);
           var userRoles = await _userManager.GetRolesAsync(user);

            var roleclaims = new List<Claim>();
            foreach (var role in userRoles)
                roleclaims.Add(new Claim("roles", role));
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
           }.Union(userClaims)
           .Union(roleclaims);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.Duration),
                signingCredentials: SigningCredentials);

            return jwtSecurityToken;


        }

    }
}
