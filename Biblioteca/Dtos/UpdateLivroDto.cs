using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Dtos;

public class UpdateLivroDto
{
    [Required(ErrorMessage = "O título do livro é obrigatório.")]
    [StringLength(200, MinimumLength = 2,
        ErrorMessage = "O título deve ter entre 2 e 200 caracteres.")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "O autor é obrigatório.")]
    [StringLength(150, MinimumLength = 2,
        ErrorMessage = "O autor deve ter entre 2 e 150 caracteres.")]
    public string Autor { get; set; } = string.Empty;

    [Required(ErrorMessage = "O ISBN é obrigatório.")]
    [StringLength(13, MinimumLength = 10,
        ErrorMessage = "O ISBN deve ter entre 10 e 13 caracteres.")]
    public string ISBN { get; set; } = string.Empty;

    [Range(1000, 2100,
        ErrorMessage = "O ano de publicação deve estar entre 1000 e 2100.")]
    public int AnoPublicacao { get; set; }

    [Range(1, int.MaxValue,
        ErrorMessage = "A categoria é obrigatória.")]
    public int CategoriaId { get; set; }

    public bool Disponivel { get; set; }
}