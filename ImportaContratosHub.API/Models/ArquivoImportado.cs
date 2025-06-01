namespace ImportaContratosHub.API.Models;

public class ArquivoImportado
{
    // id único do arquivo importado
    public int Id { get; set; }

    // nome original do arquivo enviado pelo usuário
    public string NomeArquivo { get; set; }

    // data e hora em que o arquivo foi importado
    public DateTime DataImportacao { get; set; }

    // id do usuário que realizou a importação (chave estrangeira)
    public int UsuarioId { get; set; }

    // objeto de navegação para acessar os dados do usuário diretamente
    public Usuario Usuario { get; set; }
}
