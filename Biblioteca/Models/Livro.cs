using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models;

public class Livro
{
    [Key]
    public int Id { get; set; }

    public string Titulo { get; set; } = string.Empty;

    public string Autor { get; set; } = string.Empty;

    public string ISBN { get; set; } = string.Empty;

    public int AnoPublicacao { get; set; }

    public bool Disponivel { get; set; } = true;

    public int CategoriaId { get; set; }

    // Navegação
    public virtual Categoria? Categoria { get; set; }

    public virtual ICollection<Emprestimo> Emprestimos { get; set; }
        = new List<Emprestimo>();
}
