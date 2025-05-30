namespace ImportaContratosHub.API.Models;

public class ArquivoImportado
{
    // Criando as propriedades do arquivoque será importado
    public int Id { get; set; }
    public string NomeArquivo { get; set; }
    public DateTime DataExportacao { get; set; }

    // Relaciona este arquivo com o usuario 
    public int UsuarioId { get; set; }

    // Permite acessar diretamente os dados do Usuario via EF (ex: Nome do Usuario)
    public Usuario Usuario { get; set; }

    // Um arquivo pode conter varios contratos 
    public ICollection<Contrato> Contratos { get; set; }
}
