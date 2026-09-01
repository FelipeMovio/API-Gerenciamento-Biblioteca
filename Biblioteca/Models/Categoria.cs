using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models;

public class Categoria
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Nome { get; set; } = string.Empty;

    public virtual ICollection<Livro> Livros { get; set; }
        = new List<Livro>();
}