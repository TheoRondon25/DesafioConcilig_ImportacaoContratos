using Microsoft.EntityFrameworkCore;
using ImportaContratosHub.API.Models;

namespace ImportaContratosHub.API.DataBase
{
    public class ApplicationDbContext : DbContext
    {
        // construtor que recebe as opções de configuração do banco (vem do program.cs)
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // representa a tabela de usuários no banco de dados
        public DbSet<Usuario> Usuarios { get; set; }

        // representa a tabela de arquivos importados no banco de dados
        public DbSet<ArquivoImportado> ArquivosImportados { get; set; }

        // representa a tabela de contratos no banco de dados
        public DbSet<Contrato> Contratos { get; set; }
        
    }
}
