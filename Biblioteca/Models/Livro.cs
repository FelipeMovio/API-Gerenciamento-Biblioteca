using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models;

public class Livro
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 2)]
    public string Titulo { get; set; } = string.Empty;

    [Required]
    [StringLength(150, MinimumLength = 2)]
    public string Autor { get; set; } = string.Empty;

    [Required]
    [StringLength(13, MinimumLength = 10)]
    public string ISBN { get; set; } = string.Empty;

    [Range(1000, 2100)]
    public int AnoPublicacao { get; set; }

    public bool Disponivel { get; set; } = true;

    [Range(1, int.MaxValue)]
    public int CategoriaId { get; set; }

    // Navegação
    public virtual Categoria? Categoria { get; set; }

    public virtual ICollection<Emprestimo> Emprestimos { get; set; }
        = new List<Emprestimo>();
}