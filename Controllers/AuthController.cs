using Microsoft.AspNetCore.Mvc;
using Sistema_ArgenMotos.Services;
using Sistema_ArgenMotos.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Sistema_ArgenMotos.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            await _authService.RegisterAsync(registerDTO);
            return Created();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var usuario = await _authService.AuthenticateAsync(loginDTO);
            if (usuario == null) 
                return Unauthorized(new { message = "Usuario o contraseña incorrectos" });

            var token = _authService.GenerateJwtToken(usuario);
            return Ok(new { token });
        }
    }
}
