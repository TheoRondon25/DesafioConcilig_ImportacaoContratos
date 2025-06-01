using Microsoft.AspNetCore.Mvc;
using ImportaContratosHub.API.Services;
using ImportaContratosHub.API.DataBase;
using ImportaContratosHub.API.Models;
using ImportaContratosHub.API.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ImportaContratosHub.API.Controllers;

[ApiController]
// define a rota base do controller: api/importacao
[Route("api/[controller]")] 
public class ImportacaoController : ControllerBase
{
    // injeta o serviço que processa os contratos
    private readonly IContratoService _contratoService;

    // construtor que recebe o serviço via injeção de dependência
    public ImportacaoController(IContratoService contratoService)
    {
        _contratoService = contratoService;
    }

    [Authorize] // exige autenticação para acessar esse endpoint
    [HttpPost("upload")]  // define o método como post na rota /api/importacao/upload
    [Consumes("multipart/form-data")] // informa que o endpoint consome dados no formato multipart (arquivo)
    public async Task<IActionResult> Upload([FromForm] UploadArquivoRequest request)
    {
        try
        {
            // valida se o arquivo foi enviado
            if (request.File == null || request.File.Length == 0)
                return BadRequest("Arquivo inválido.");

            // recupera o id do usuário logado a partir do token jwt
            int usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            // chama o serviço que processa o csv e salva no banco
            await _contratoService.ProcessarCSVAsync(request.File.OpenReadStream(), usuarioId, request.File.FileName);

            // retorna sucesso para o usuário
            return Ok("Arquivo importado com sucesso!");
        }
        catch (FormatException ex)
        {
            return BadRequest(new { erro = "Erro de formatação: verifique os dados do CSV.", detalhes = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { erro = "Erro interno ao processar o arquivo.", detalhes = ex.Message });
        }
      
    }
}