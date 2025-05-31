using Microsoft.EntityFrameworkCore;
using ImportaContratosHub.API.Models;

namespace ImportaContratosHub.API.DataBase
{
    public class ApplicationDbContext : DbContext
    {
        // Construtor padrão para receber configurações do Program.cs
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<ArquivoImportado> ArquivosImportados { get; set; }
        public DbSet<Contrato> Contratos { get; set; }
        
    }
}
