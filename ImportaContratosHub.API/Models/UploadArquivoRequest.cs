using Microsoft.AspNetCore.Http;

namespace ImportaContratosHub.API.Models;
public class UploadArquivoRequest
{
    public IFormFile Arquivo { get; set; }
}


