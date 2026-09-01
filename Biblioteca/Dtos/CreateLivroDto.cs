using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Dtos;

public class CreateLivroDto
{
    [Required(ErrorMessage = "O titulo do livro es obrigatorio")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "O autor es obrigatorio")]
    public string Autor { get; set; } = string.Empty;

    [Required(ErrorMessage = "")]
    public string ISBN { get; set; } = string.Empty;

    [Required(ErrorMessage = "")]
    public int AnoPublicacao { get; set; }

    [Required(ErrorMessage = "")]
    public int CategoriaId { get; set; }
}
