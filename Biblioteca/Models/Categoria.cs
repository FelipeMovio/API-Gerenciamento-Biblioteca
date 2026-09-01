using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models;

public class Categoria
{
    [Key]
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public virtual ICollection<Livro> Livros { get; set; }
        = new List<Livro>();
}
