using PetStore.DTOS;

namespace PetStore.Services.AuthService
{
    public interface IAuthService
    {
        Task<AuthDTO> RegisterAsync(RegisterDTO registerDTO);
        Task<AuthDTO> LoginAsync(LoginDTO LoginDTO);

    }
}
