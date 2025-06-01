namespace ImportaContratosHub.API.Models;

public class ArquivoImportado
{
    public int Id { get; set; }
    public string NomeArquivo { get; set; }
    public DateTime DataImportacao { get; set; }
    
    // Relaciona este arquivo importado com o usuario que fez a importação
    public int UsuarioId { get; set; }

    // Permite acessar o usuario diretamente
    public Usuario Usuario { get; set; }
}
