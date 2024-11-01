using Sistema_ArgenMotos.Entidades;
using Sistema_ArgenMotos.DTOs;

namespace Sistema_ArgenMotos.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterDTO registerDTO);
        Task<Usuario> AuthenticateAsync(LoginDTO loginDTO);
        string GenerateJwtToken(Usuario usuario);
    }
}
