using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetStore.DTOS;
using PetStore.Services.AuthService;

namespace PetStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var authResult = await _authService.RegisterAsync(model);
            if (!authResult.IsAuthenticated) return BadRequest(authResult.Message);

            return Ok(authResult);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var authResult = await _authService.LoginAsync(model);
            if (!authResult.IsAuthenticated) return BadRequest(authResult.Message);

            return Ok(authResult);
        }


    }
}
