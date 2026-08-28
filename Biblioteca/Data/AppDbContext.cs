using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opts)
        : base(opts)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
    }

    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Emprestimo> Emprestimos { get; set; }
    public DbSet<Livro> Livros { get; set; }

    public DbSet<Usuario> Usuarios { get; set; }



}
