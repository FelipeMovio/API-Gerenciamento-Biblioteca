using Biblioteca.Domain.Models.LivroMod;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Domain.Models.CategoriaMod;

public class Categoria
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Nome { get; set; } = string.Empty;

    public virtual ICollection<Livro> Livros { get; set; }
        = new List<Livro>();
}