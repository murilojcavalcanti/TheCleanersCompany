using Microsoft.EntityFrameworkCore;
using BackEnd.Domain.Entities;

namespace BackEnd.Infra.DbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public DbSet<Cliente> Clientes { get; set; }
    }
}
