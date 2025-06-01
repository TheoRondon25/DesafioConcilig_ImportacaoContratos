using System.ComponentModel.DataAnnotations;

namespace ImportaContratosHub.API.Models.Requests
{
    public class UploadArquivoRequest
    {
        // representa o arquivo enviado pelo usuário via formulário
        [Required]
        public IFormFile File { get; set; }
    }
}
