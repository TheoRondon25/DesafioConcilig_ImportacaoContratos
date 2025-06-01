using System.IO;

namespace ImportaContratosHub.API.Services
{
    public interface IContratoService
    {
        Task ProcessarCSVAsync(Stream stream, int usuarioId, string nomeArquivo);
    }
}
