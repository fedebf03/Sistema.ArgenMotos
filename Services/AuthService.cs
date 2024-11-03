using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Sistema_ArgenMotos.DTOs;
using Sistema_ArgenMotos.Entidades;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sistema_ArgenMotos.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthService(ApplicationDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task RegisterAsync(RegisterDTO registerDTO)
        {
            var vendedor = await _context.Vendedores.FirstOrDefaultAsync(v => v.Email == registerDTO.EmailVendedor);

            if (vendedor == null)
                throw new Exception($"No existe ningún vendedor con email {registerDTO.EmailVendedor}");

            var usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.VendedorId == vendedor.VendedorId);

            if (usuarioExistente != null)
                throw new Exception($"Ya existe un usuario para el vendedor con ID {vendedor.VendedorId} y email {registerDTO.EmailVendedor}");

            var usuario = new Usuario
            {
                VendedorId = vendedor.VendedorId
            };

            CreatePasswordHash(registerDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);
            usuario.PasswordHash = passwordHash;
            usuario.PasswordSalt = passwordSalt;

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario> AuthenticateAsync(LoginDTO loginDTO)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Vendedor)
                .FirstOrDefaultAsync(u => u.Vendedor.Email == loginDTO.Email);

            if (usuario == null || !VerifyPasswordHash(loginDTO.Password, usuario.PasswordHash, usuario.PasswordSalt))
                throw new Exception("El usuario o la contraseña no son válidos.");

            return usuario;
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash);
            }
        }

        public string GenerateJwtToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:Token"]);  // Acceso a la clave secreta desde el archivo de configuración
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Name, usuario.VendedorId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
