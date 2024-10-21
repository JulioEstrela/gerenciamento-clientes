using Crud.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Crud.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EnderecoCobranca>();
            modelBuilder.Entity<EnderecoFiscal>();
            modelBuilder.Entity<EnderecoEntrega>();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; } 
    }
}
