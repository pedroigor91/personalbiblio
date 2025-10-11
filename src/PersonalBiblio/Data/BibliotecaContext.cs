using Microsoft.EntityFrameworkCore;
using PersonalBiblio.Models;

namespace PersonalBiblio.Data;

public class BibliotecaContext : DbContext
{
    public BibliotecaContext(DbContextOptions<BibliotecaContext> options) : base(options)
    {
    }

    public DbSet<Livro> Livros { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuração da entidade Livro
        modelBuilder.Entity<Livro>(entity =>
        {
            entity.ToTable("livros");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Titulo).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Autor).IsRequired().HasMaxLength(150);
            entity.Property(e => e.ISBN).HasMaxLength(13);
            entity.Property(e => e.Status).HasConversion<int>();

            // Relacionamento com Categoria
            entity.HasOne(e => e.Categoria)
                .WithMany(c => c.Livros)
                .HasForeignKey(e => e.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configuração da entidade Categoria
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.ToTable("categorias");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Descricao).HasMaxLength(500);
        });

        // Dados iniciais (Seed)
        modelBuilder.Entity<Categoria>().HasData(
            new Categoria { Id = 1, Nome = "Ficção", Descricao = "Livros de ficção em geral" },
            new Categoria { Id = 2, Nome = "Não-Ficção", Descricao = "Livros baseados em fatos reais" },
            new Categoria { Id = 3, Nome = "Tecnologia", Descricao = "Livros sobre tecnologia e programação" },
            new Categoria { Id = 4, Nome = "Biografias", Descricao = "Histórias de vida" },
            new Categoria { Id = 5, Nome = "Autoajuda", Descricao = "Desenvolvimento pessoal" }
        );
    }
}
