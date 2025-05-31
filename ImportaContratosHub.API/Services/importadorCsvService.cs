using CsvHelper; // pacote para ler CSV
using ImportaContratosHub.API.DataBase;
using ImportaContratosHub.API.Models; 
using System.Globalization; // cultura para leitura de CSV


namespace ImportaContratosHub.API.Services;

// Serviço responsável por importar contratos a partir de um arquivo CSV.
public class ImportadorCsvService
{
    private readonly ApplicationDbContext _context;

    // Injeta o contexto do banco de dados no serviço.
    public ImportadorCsvService(ApplicationDbContext context)
    {
        _context = context;
    }

    // Lê e importa os contratos contidos em um arquivo CSV para o banco de dados.
    public async Task<int> ImportarContratos(IFormFile arquivo, int arquivoImportadoID)
    {
        // Lista que armazenará os contratos lidos do CSV
        var contratos = new List<Contrato>();

        // Abre o arquivo CSV para leitura
        using (var reader = new StreamReader(arquivo.OpenReadStream()))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            // Lê o cabeçalho da primeira linha (ex: Nome, CPF, Produto, etc.)
            csv.Read();
            csv.ReadHeader();

            // Lê linha por linha do CSV e mapeia os dados para objetos Contrato
            while (csv.Read())
            {
                var contrato = new Contrato
                {
                    NomeCliente = csv.GetField("Nome"),
                    Cpf = csv.GetField("CPF"),
                    NumeroContrato = csv.GetField("Contrato"),
                    Produto = csv.GetField("Produto"),
                    DataVencimento = DateTime.Parse(csv.GetField("Vencimento")),
                    Valor = decimal.Parse(csv.GetField("Valor")),
                    ArquivoImportadoId = arquivoImportadoID // associa com o arquivo importado
                };

                contratos.Add(contrato);
            }
        }

        // Salva os contratos no banco de dados
        await _context.Contratos.AddRangeAsync(contratos);
        await _context.SaveChangesAsync();

        return contratos.Count; // Retorna o total de contratos importados
    }
}
