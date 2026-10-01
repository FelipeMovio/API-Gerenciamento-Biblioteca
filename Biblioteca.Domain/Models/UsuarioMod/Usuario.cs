using Biblioteca.Domain.Models.EmprestimoMod;
using Biblioteca.Domain.Models.UsuarioMod.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Domain.Models.UsuarioMod;

public class Usuario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(150, MinimumLength = 2)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(200)]
    public string Email { get; set; } = string.Empty;

    public bool EmailConfirmado { get; set; } = false;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    public TipoUsuario Tipo { get; set; } = TipoUsuario.Cliente;

    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

    // Navegação
    public virtual ICollection<Emprestimo> Emprestimos { get; set; }
        = new List<Emprestimo>();
}