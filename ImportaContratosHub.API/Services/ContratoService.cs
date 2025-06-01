using CsvHelper;
using CsvHelper.Configuration;
using ImportaContratosHub.API.DataBase;
using ImportaContratosHub.API.Models;
using ImportaContratosHub.API.Services;
using System.Globalization;
using System.Text;

namespace ImportadorContratos.Services;

public class ContratoService : IContratoService
{
    // injeta o contexto do banco de dados
    private readonly ApplicationDbContext _context;

    public ContratoService(ApplicationDbContext context)
    {
        _context = context;
    }

    // método responsável por processar o csv e salvar no banco
    public async Task ProcessarCSVAsync(Stream stream, int usuarioId, string nomeArquivo)
    {
        // lê o arquivo com a codificação windows-1252 (suporta acentos)
        using var reader = new StreamReader(stream, Encoding.GetEncoding("windows-1252"));

        // configurações para o csv helper: separador ';' e ignora validações de header e campos ausentes
        var config = new CsvConfiguration(new CultureInfo("pt-BR"))
        {
            Delimiter = ";", 
            HeaderValidated = null,
            MissingFieldFound = null
        };

        // inicializa o leitor csv com as configurações definidas
        using var csv = new CsvReader(reader, config);

        // lê e converte todos os registros para o modelo ContratoCSV
        var registros = csv.GetRecords<ContratoCSV>().ToList();

        // cria o registro do arquivo importado com o nome, data e usuário responsável
        var arquivo = new ArquivoImportado
        {
            NomeArquivo = nomeArquivo,
            DataImportacao = DateTime.Now,
            UsuarioId = usuarioId
        };

        // adiciona o registro do arquivo importado no banco
        _context.ArquivosImportados.Add(arquivo);
        await _context.SaveChangesAsync(); // salva para obter o id do arquivo

        // transforma os registros do csv em objetos Contrato para o banco
        var contratos = registros.Select(r => new Contrato
        {
            NomeCliente = r.Nome,
            CPF = r.CPF,
            NumeroContrato = r.Contrato,
            Produto = r.Produto,
            DataVencimento = r.Vencimento,
            Valor = r.Valor,
            ArquivoImportadoId = arquivo.Id, // associa com o arquivo importado
            UsuarioId = usuarioId,
            DataImportacao = DateTime.Now
        }).ToList();

        // adiciona todos os contratos no banco
        _context.Contratos.AddRange(contratos);
        await _context.SaveChangesAsync(); // salva tudo
    }
}