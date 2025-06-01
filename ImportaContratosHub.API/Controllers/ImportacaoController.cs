using Microsoft.AspNetCore.Mvc;
using ImportaContratosHub.API.Services;
using ImportaContratosHub.API.DataBase;
using ImportaContratosHub.API.Models;
using ImportaContratosHub.API.Models.Requests;

namespace ImportaContratosHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImportacaoController : ControllerBase
{
    private readonly IContratoService _contratoService;

    public ImportacaoController(IContratoService contratoService)
    {
        _contratoService = contratoService;
    }

    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Upload([FromForm] UploadArquivoRequest request)
    {
        if (request.File == null || request.File.Length == 0)
            return BadRequest("Arquivo inválido.");

        int usuarioId = 1; // realizar a autenticacao depois que tudo funcionar
        await _contratoService.ProcessarCSVAsync(request.File.OpenReadStream(), usuarioId, request.File.FileName);

        return Ok("Arquivo importado com sucesso!");        
    }
}