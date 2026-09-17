using Biblioteca.Domain.Models.CategoriaMod;
using Biblioteca.Domain.Models.EmprestimoMod;
using Biblioteca.Domain.Models.LivroMod;
using Biblioteca.Domain.Models.UsuarioMod;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infrastructure.Modulos._Core.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opts)
        : base(opts)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Usuario>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }

    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Emprestimo> Emprestimos { get; set; }
    public DbSet<Livro> Livros { get; set; }

    public DbSet<Usuario> Usuarios { get; set; }



}
