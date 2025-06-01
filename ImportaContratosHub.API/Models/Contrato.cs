namespace ImportaContratosHub.API.Models;

public class Contrato
{
    // Criando as propriedades do arquivoque será importado
    public int Id { get; set; }
    public string NomeCliente { get; set; }
    public string CPF { get; set; }
    public string NumeroContrato { get; set; }
    public string Produto { get; set; }
    public DateTime DataVencimento { get; set; }
    public decimal Valor { get; set; }

    // Relaciona este contrato com o arquivo de onde ele veio
    public int ArquivoImportadoId { get; set; }
    // Permite acessar diretamente os dados do ArquivoImportado via EF (ex: Nome do arquivo)
    public ArquivoImportado ArquivoImportado { get; set; }

    // Relaciona este contrato com o usuario
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    public DateTime DataImportacao { get; set; }
}
