using Biblioteca.Domain.Models.LivroMod;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Domain.Models.CategoriaMod;

public class Categoria
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Nome { get; private set; } = string.Empty;

    public virtual ICollection<Livro> Livros { get; private set; }
        = new List<Livro>();

    // Construtor utilizado pelo Entity Framework
    private Categoria() { }

    // Construtor para criação pela aplicação
    public Categoria(string nome)
    {
        AlterarNome(nome);
    }

    public void AlterarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome da categoria é obrigatório.");

        nome = nome.Trim();

        if (nome.Length < 2)
            throw new ArgumentException(
                "O nome da categoria deve ter pelo menos 2 caracteres.");

        if (nome.Length > 100)
            throw new ArgumentException(
                "O nome da categoria deve ter no máximo 100 caracteres.");

        Nome = nome;
    }
}