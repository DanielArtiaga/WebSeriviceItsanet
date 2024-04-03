using Api_clientes.Domain;
using Microsoft.EntityFrameworkCore;

namespace Api_clientes.Infraestructure
{
    public class AplicationDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }
    }
}
