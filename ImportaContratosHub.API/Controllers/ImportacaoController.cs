using Microsoft.AspNetCore.Mvc;
using ImportaContratosHub.API.Services;
using ImportaContratosHub.API.DataBase;
using ImportaContratosHub.API.Models;

namespace ImportaContratosHub.API.Controllers;

// Define que esta classe é uma API Controller e deve responder a requisições REST
[ApiController]
[Route("api/[controller]")] // rota da api: api/importacao
public class ImportacaoController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ImportadorCsvService _importadorCsvService;

    // Construtor que injeta o contexto do banco de dados e o servico de importacao
    public ImportacaoController(ApplicationDbContext context, ImportadorCsvService importadorCsvService)
    {
        _context = context;
        _importadorCsvService = importadorCsvService;
    }

    // Endpoint POST que recebe um arquivo CSV e importa os contratos
    [HttpPost("upload")]
    [Consumes("multipart/form-data")] // Indica que o endpoint consome dados de formulário
    public async Task<IActionResult> UploadCsv([FromForm] UploadArquivoRequest request)
    {
        var arquivo = request.Arquivo;

        // Verifica se o arquivo foi fornecido
        if (arquivo == null || arquivo.Length == 0)
            return BadRequest("Arquivo não fornecido.");

        // autenticaçao do usuario
        int usuarioId = 1;

        // Cria o registro do arquivo importado no banco de dados
        var arquivoImportado = new ArquivoImportado
        {
            NomeArquivo = arquivo.FileName,
            DataExportacao = DateTime.Now,
            UsuarioId = usuarioId
        };

        // Salva o registro do arquivo no banco
        await _context.ArquivosImportados.AddAsync(arquivoImportado);
        await _context.SaveChangesAsync();

        // Chama o servico responsavel por ler e importar os contratos do CSV
        var totalImportados = await _importadorCsvService.ImportarContratos(arquivo, arquivoImportado.Id);

        // Retorna uma resposta 200 OK com resumo da importaçao
        return Ok(new
        {
            arquivo = arquivo.FileName,
            totalImportados
        });
    }
}
