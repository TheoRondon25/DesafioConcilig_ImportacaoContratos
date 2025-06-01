using Microsoft.AspNetCore.Mvc;
using ImportaContratosHub.API.Services;
using ImportaContratosHub.API.DataBase;
using ImportaContratosHub.API.Models;
using ImportaContratosHub.API.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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

    [Authorize]
    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Upload([FromForm] UploadArquivoRequest request)
    {

        if (request.File == null || request.File.Length == 0)
            return BadRequest("Arquivo inválido.");

        int usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        await _contratoService.ProcessarCSVAsync(request.File.OpenReadStream(), usuarioId, request.File.FileName);

        return Ok("Arquivo importado com sucesso!");        
    }
}