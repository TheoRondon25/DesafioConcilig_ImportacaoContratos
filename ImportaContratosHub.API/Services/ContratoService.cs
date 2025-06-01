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
    private readonly ApplicationDbContext _context;

    public ContratoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task ProcessarCSVAsync(Stream stream, int usuarioId, string nomeArquivo)
    {
        using var reader = new StreamReader(stream, Encoding.GetEncoding("windows-1252")); // para aceitar acentos 
        var config = new CsvConfiguration(new CultureInfo("pt-BR"))
        {
            Delimiter = ";", 
            HeaderValidated = null,
            MissingFieldFound = null
        };

        using var csv = new CsvReader(reader, config);
        var registros = csv.GetRecords<ContratoCSV>().ToList();

        var arquivo = new ArquivoImportado
        {
            NomeArquivo = nomeArquivo,
            DataImportacao = DateTime.Now,
            UsuarioId = usuarioId
        };

        _context.ArquivosImportados.Add(arquivo);
        await _context.SaveChangesAsync();

        var contratos = registros.Select(r => new Contrato
        {
            NomeCliente = r.Nome,
            CPF = r.CPF,
            NumeroContrato = r.Contrato,
            Produto = r.Produto,
            DataVencimento = r.Vencimento,
            Valor = r.Valor,
            ArquivoImportadoId = arquivo.Id,
            UsuarioId = usuarioId,
            DataImportacao = DateTime.Now
        }).ToList();

        _context.Contratos.AddRange(contratos);
        await _context.SaveChangesAsync();
    }
}