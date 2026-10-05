using Biblioteca.Domain.Models.CategoriaMod;
using Biblioteca.Domain.Models.EmprestimoMod;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Domain.Models.LivroMod;

public class Livro
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    [Required]
    [StringLength(200, MinimumLength = 2)]
    public string Titulo { get; private set; } = string.Empty;

    [Required]
    [StringLength(150, MinimumLength = 2)]
    public string Autor { get; private set; } = string.Empty;

    [Required]
    [StringLength(13, MinimumLength = 10)]
    public string ISBN { get; private set; } = string.Empty;

    [Range(1000, 2100)]
    public int AnoPublicacao { get; private set; }

    public bool Disponivel { get; private set; } = true;

    [Range(1, int.MaxValue)]
    public int CategoriaId { get; private set; }

    // Navegação
    public virtual Categoria? Categoria { get; private set; }

    public virtual ICollection<Emprestimo> Emprestimos { get; private set; }
        = new List<Emprestimo>();

    // Construtor utilizado pelo Entity Framework
    private Livro() { }

    // Construtor para criação pela aplicação
    public Livro(
        string titulo,
        string autor,
        string isbn,
        int anoPublicacao,
        int categoriaId)
    {
        AlterarDados(titulo, autor, isbn, anoPublicacao, categoriaId);
        Disponivel = true;
    }

    public void AlterarDados(
        string titulo,
        string autor,
        string isbn,
        int anoPublicacao,
        int categoriaId)
    {
        if (string.IsNullOrWhiteSpace(titulo) || titulo.Trim().Length < 2)
            throw new ArgumentException("O título deve possuir pelo menos 2 caracteres.");

        if (titulo.Trim().Length > 200)
            throw new ArgumentException("O título deve possuir no máximo 200 caracteres.");

        if (string.IsNullOrWhiteSpace(autor) || autor.Trim().Length < 2)
            throw new ArgumentException("O autor deve possuir pelo menos 2 caracteres.");

        if (autor.Trim().Length > 150)
            throw new ArgumentException("O autor deve possuir no máximo 150 caracteres.");

        if (string.IsNullOrWhiteSpace(isbn) || isbn.Length < 10 || isbn.Length > 13)
            throw new ArgumentException("O ISBN deve possuir entre 10 e 13 caracteres.");

        if (anoPublicacao < 1000 || anoPublicacao > 2100)
            throw new ArgumentException("O ano de publicação deve estar entre 1000 e 2100.");

        if (categoriaId <= 0)
            throw new ArgumentException("A categoria informada é inválida.");

        Titulo = titulo.Trim();
        Autor = autor.Trim();
        ISBN = isbn.Trim();
        AnoPublicacao = anoPublicacao;
        CategoriaId = categoriaId;
    }

    public void MarcarComoDisponivel()
    {
        Disponivel = true;
    }

    public void MarcarComoIndisponivel()
    {
        Disponivel = false;
    }
}