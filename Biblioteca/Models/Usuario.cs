using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models;

public class Usuario
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(150, MinimumLength = 2)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(200)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

    // Navegação
    public virtual ICollection<Emprestimo> Emprestimos { get; set; }
        = new List<Emprestimo>();
}