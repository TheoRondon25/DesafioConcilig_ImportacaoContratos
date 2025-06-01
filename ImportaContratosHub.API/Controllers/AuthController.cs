using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ImportaContratosHub.API.DataBase;
using ImportaContratosHub.API.Models.Requests;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ImportaContratosHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestAuth request)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == request.Email);
            if (usuario == null)
                return Unauthorized("Usuário não encontrado.");

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KEYsecretTEST@2025-contratosconcilig"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: creds
            );

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}
