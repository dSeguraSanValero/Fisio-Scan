using Microsoft.AspNetCore.Mvc;
using FisioScan.Models;
using FisioScan.Business;
using FisioSolution.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FisioScan.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IPhysioService _physioService;
        private readonly IConfiguration _configuration;

        public AuthController(IPhysioService physioService, IConfiguration configuration)
        {
            _physioService = physioService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO loginDto)
        {
            // Verificar que los datos de inicio de sesión no sean nulos
            if (loginDto == null || string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
                return BadRequest("Email y contraseña requeridos.");

            // Validar usuario
            var physio = _physioService.ValidatePhysio(loginDto.Email, loginDto.Password);
            if (physio == null)
                return Unauthorized("Credenciales incorrectas.");

            // Generar el token JWT
            var token = GenerateJwtToken(physio);
            return Ok(new { token });
        }

        private string GenerateJwtToken(Physio physio)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, physio.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
