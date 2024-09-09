using API.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Servico> Servicos { get; set; }
        
        public DbSet<Categoria> Categorias { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //Criação de relacionamento nas classes profile
        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relacionamento 1-N entre Categoria e Servico
            modelBuilder.Entity<Servico>()
                .HasOne(s => s.Categoria)
                .WithMany(c => c.Servicos)
                .HasForeignKey(s => s.CategoriaId)
                .OnDelete(DeleteBehavior.Cascade); // Exclusão em cascata

            // Relacionamento 1-N entre User e Servico
            modelBuilder.Entity<Servico>()
                .HasOne(s => s.User)
                .WithMany(u => u.Servicos)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Exclusão em cascata

            // Relacionamento 1-N entre User e Categoria (caso necessário)
            modelBuilder.Entity<Categoria>()
                .HasOne(c => c.User)
                .WithMany(u => u.Categorias)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Exclusão em cascata
        }*/
    }
}
