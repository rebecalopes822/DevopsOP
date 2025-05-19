using Microsoft.EntityFrameworkCore;
using OdontoPrevAPI.Models;

namespace OdontoPrevAPI.Data
{
    public class OdontoPrevContext : DbContext
    {
        public OdontoPrevContext(DbContextOptions<OdontoPrevContext> options) : base(options) { }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Tratamento> Tratamentos { get; set; }
        public DbSet<Sinistro> Sinistros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("RM553764"); 
        }
    }
}
