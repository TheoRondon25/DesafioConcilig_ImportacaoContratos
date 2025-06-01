namespace ImportaContratosHub.API.Models;

public class Contrato
{
    // id único do contrato
    public int Id { get; set; }

    // nome do cliente associado ao contrato
    public string NomeCliente { get; set; }

    // cpf do cliente
    public string CPF { get; set; }

    // número do contrato (deve ser único se desejar controlar duplicidade)
    public string NumeroContrato { get; set; }

    // tipo de produto do contrato (ex: financiamento, consórcio)
    public string Produto { get; set; }

    // data de vencimento do contrato
    public DateTime DataVencimento { get; set; }

    // valor do contrato
    public decimal Valor { get; set; }

    // id do arquivo do qual o contrato foi importado (chave estrangeira)
    public int ArquivoImportadoId { get; set; }

    // objeto de navegação para acessar dados do arquivo importado
    public ArquivoImportado ArquivoImportado { get; set; }

    // id do usuário que importou o contrato (chave estrangeira)
    public int UsuarioId { get; set; }

    // objeto de navegação para acessar dados do usuário que importou
    public Usuario Usuario { get; set; }

    // data e hora em que o contrato foi importado
    public DateTime DataImportacao { get; set; }
}
