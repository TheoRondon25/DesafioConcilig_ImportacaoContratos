namespace ImportaContratosHub.API.Models
{
    public class ContratoCSV
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Contrato { get; set; }
        public string Produto { get; set; }
        public DateTime Vencimento { get; set; }
        public decimal Valor { get; set; }
    }
}
