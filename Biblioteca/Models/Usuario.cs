using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models;

public class Usuario
{
    [Key]
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

    // Navegação
    public virtual ICollection<Emprestimo> Emprestimos { get; set; }
        = new List<Emprestimo>();
}