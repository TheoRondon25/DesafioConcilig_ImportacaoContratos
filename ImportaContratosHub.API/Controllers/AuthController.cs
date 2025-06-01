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

        // injeta o contexto do banco de dados para acessar a tabela de usuários
        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        // endpoint post para realizar o login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestAuth request)
        {
            try
            {
                // busca o usuário pelo e-mail informado
                var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == request.Email);

                // se não encontrar, retorna 401 (unauthorized)
                if (usuario == null)
                    return Unauthorized("Usuário não encontrado.");

                // define os dados que vão dentro do token (claims)
                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
                };

                // cria a chave de segurança usada para assinar o token
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KEYsecretTEST@2025-contratosconcilig"));

                // define o algoritmo de assinatura
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                // gera o token jwt com expiração de 24 horas
                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddHours(24),
                    signingCredentials: creds
                );

                // retorna o token gerado no corpo da resposta
                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao processar o login.", detalhes = ex.Message });
            }
            
        }
    }
}
