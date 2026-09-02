namespace Biblioteca.Dtos;

public class ReadLivroDto
{
    public int Id { get; set; }

    public string Titulo { get; set; } = string.Empty;

    public string Autor { get; set; } = string.Empty;

    public string ISBN { get; set; } = string.Empty;

    public int AnoPublicacao { get; set; }

    public int CategoriaId { get; set; }

    public bool Disponivel { get; set; }

    public CategoriaLivroDto? Categoria { get; set; }
}
