namespace Biblioteca.Models;

public class Livro
{
    public int Id { get; set; }

    public string Titulo { get; set; } = string.Empty;

    public string Autor { get; set; } = string.Empty;

    public string ISBN { get; set; } = string.Empty;

    public int AnoPublicacao { get; set; }

    public int CategoriaId { get; set; }

    public bool Disponivel { get; set; } = true;

    // Navegação
    public virtual Categoria? Categoria { get; set; }

    public virtual ICollection<Emprestimo> Emprestimos { get; set; }
        = new List<Emprestimo>();
}
