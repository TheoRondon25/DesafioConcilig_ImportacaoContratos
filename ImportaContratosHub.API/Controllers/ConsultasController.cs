using ImportaContratosHub.API.DataBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImportaContratosHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultasController : Controller
    {
        private readonly ApplicationDbContext _context;

        // injeta o contexto do banco de dados para realizar consultas
        public ConsultasController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Consulta todos os arquivos importados junto com o nome do usuário que realizou a importação.
        /// </summary>
        [Authorize] // exige autenticação via token
        [HttpGet("arquivos-importados")]
        public async Task<IActionResult> GetArquivosImportados()
        {
            try
            {
                // busca os arquivos e inclui os dados do usuário
                var arquivos = await _context.ArquivosImportados
                    .Include(a => a.Usuario) // inclui o relacionamento com o usuário
                    .Select(a => new
                    {
                        a.Id,
                        a.NomeArquivo,
                        a.DataImportacao,
                        Usuario = a.Usuario.Nome
                    })
                    .ToListAsync();

                // retorna a lista no formato json
                return Ok(arquivos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "erro ao consultar arquivos importados", detalhe = ex.Message });
            }            
        }

        /// <summary>
        /// Consulta todos os contratos importados com informações básicas.
        /// </summary>
        [Authorize]
        [HttpGet("contratos")]
        public async Task<IActionResult> GetContratos()
        {
            try
            {
                // busca todos os contratos e seleciona apenas os campos relevantes
                var contratos = await _context.Contratos
                    .Select(c => new
                    {
                        c.Id,
                        c.NomeCliente,
                        c.CPF,
                        c.NumeroContrato,
                        c.Produto,
                        c.DataVencimento,
                        c.Valor,
                        c.DataImportacao
                    })
                    .ToListAsync();

                // retorna os contratos encontrados
                return Ok(contratos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "erro ao consultar contratos", detalhe = ex.Message });
            }            
        }

        /// <summary>
        /// Consulta o valor total dos contratos por cliente e o maior atraso em dias (entre vencimento e hoje).
        /// </summary>
        [Authorize]
        [HttpGet("clientes/analise")]
        public async Task<IActionResult> GetAnalisePorCliente()
        {
            try
            {
                // agrupa os contratos por cliente (nome e cpf)
                var analise = await _context.Contratos
                    .GroupBy(c => new { c.NomeCliente, c.CPF })
                    .Select(g => new
                    {
                        g.Key.NomeCliente,
                        g.Key.CPF,
                        ValorTotal = g.Sum(c => c.Valor), // soma dos valores dos contratos
                        MaiorAtrasoDias = g.Max(c => EF.Functions.DateDiffDay(c.DataVencimento, DateTime.Now)) // diferença em dias entre vencimento e data atual
                    })
                    .ToListAsync();

                // retorna o resultado da análise
                return Ok(analise);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "erro ao consultar análise por cliente", detalhe = ex.Message });
            }
        }
    }
}
