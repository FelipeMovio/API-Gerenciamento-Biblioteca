
using Biblioteca.Domain.Models.EmprestimoMod;
using Biblioteca.Domain.Models.UsuarioMod.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Domain.Models.UsuarioMod;

public class Usuario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    [Required]
    [StringLength(150, MinimumLength = 2)]
    public string Nome { get; private set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(200)]
    public string Email { get; private set; } = string.Empty;

    public bool EmailConfirmado { get; private set; } = false;

    [Required]
    public string PasswordHash { get; private set; } = string.Empty;

    public TipoUsuario Tipo { get; private set; } = TipoUsuario.Cliente;

    public DateTime DataCadastro { get; private set; } = DateTime.UtcNow;

    // Navegação
    public virtual ICollection<Emprestimo> Emprestimos { get; private set; }
        = new List<Emprestimo>();

    // Construtor utilizado pelo Entity Framework Core
    private Usuario() { }

    // Construtor para criação de um usuário
    public Usuario(string nome, string email)
    {
        Nome = nome;
        Email = email;
        Tipo = TipoUsuario.Cliente;
        EmailConfirmado = false;
        DataCadastro = DateTime.UtcNow;
    }


    public void DefinirPasswordHash(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("O hash da senha é obrigatório.");

        PasswordHash = passwordHash;
    }

    public void ConfirmarEmail()
    {
        EmailConfirmado = true;
    }

    public void AlterarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome é obrigatório.");

        Nome = nome.Trim();
    }

    public void AlterarEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("O e-mail é obrigatório.");

        email = email.Trim().ToLowerInvariant();

        if (Email != email)
        {
            Email = email;
            EmailConfirmado = false;
        }
    }

    public void AlterarSenha(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("O hash da senha é obrigatório.");

        PasswordHash = passwordHash;
    }

    public void PromoverParaAdministrador()
    {
        Tipo = TipoUsuario.Administrador;
    }

    public void RebaixarParaCliente()
    {
        Tipo = TipoUsuario.Cliente;
    }
}