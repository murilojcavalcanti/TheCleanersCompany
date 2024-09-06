using API.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> Opts):base(Opts) {}
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<API.Data.Entities.Categoria> Categoria { get; set; } = default!;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Relacionamento 1-N entre Categoria e Servico
        modelBuilder.Entity<Servico>()
            .HasOne(s => s.Categoria)
            .WithMany(c => c.Servicos)
            .HasForeignKey(s => s.CategoriaId);

        // Relacionamento 1-N entre User e Servico
        modelBuilder.Entity<Servico>()
            .HasOne(s => s.User)
            .WithMany(u => u.Servicos)
            .HasForeignKey(s => s.UserId);
    }
    }
}
