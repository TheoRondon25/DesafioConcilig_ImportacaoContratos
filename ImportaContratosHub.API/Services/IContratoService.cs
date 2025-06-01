using System.IO;

namespace ImportaContratosHub.API.Services
{
    // define a interface do serviço de contratos
    public interface IContratoService
    {
        // método responsável por processar um arquivo csv e salvar os dados no banco
        // recebe o stream do arquivo, o id do usuário autenticado e o nome do arquivo original
        Task ProcessarCSVAsync(Stream stream, int usuarioId, string nomeArquivo);
    }
}
