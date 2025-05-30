namespace ImportaContratosHub.API.Models;

public class Usuario
{
    // Criando as propriedades de Usuario
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }

    // Um usuario pode importar vários arquivos
    public ICollection<ArquivoImportado> ArquivosImportados { get; set; }
}
