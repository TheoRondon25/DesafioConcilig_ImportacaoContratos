namespace ImportaContratosHub.API.Models
{
    public class ContratoCSV
    {
        // nome do cliente no arquivo csv
        public string Nome { get; set; }

        // cpf do cliente no arquivo csv
        public string CPF { get; set; }

        // número do contrato no arquivo csv
        public string Contrato { get; set; }

        // tipo de produto no arquivo csv
        public string Produto { get; set; }

        // data de vencimento do contrato no arquivo csv
        public DateTime Vencimento { get; set; }

        // valor do contrato no arquivo csv
        public decimal Valor { get; set; }
    }
}
