using System.ComponentModel.DataAnnotations;

namespace ImportaContratosHub.API.Models.Requests
{
    public class UploadArquivoRequest
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
