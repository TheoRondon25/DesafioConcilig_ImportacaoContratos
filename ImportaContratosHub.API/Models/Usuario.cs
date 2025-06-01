namespace ImportaContratosHub.API.Models;

public class Usuario
{
    // id único do usuário
    public int Id { get; set; }

    // nome completo do usuário
    public string Nome { get; set; }

    // e-mail do usuário, utilizado para login
    public string Email { get; set; }
}
