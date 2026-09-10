namespace Biblioteca.Application.Commands;

public class CreateLivroCommand
{
    public string Titulo { get; set; } = string.Empty;

    public string Autor { get; set; } = string.Empty;

    public string ISBN { get; set; } = string.Empty;

    public int AnoPublicacao { get; set; }

    public int CategoriaId { get; set; }
}
