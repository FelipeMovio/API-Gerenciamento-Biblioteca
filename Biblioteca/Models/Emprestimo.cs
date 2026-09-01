using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models;

public class Emprestimo
{
    [Key]
    public int Id { get; set; }

    public int LivroId { get; set; }

    public int UsuarioId { get; set; }

    public DateTime DataEmprestimo { get; set; }

    public DateTime DataDevolucaoPrevista { get; set; }

    public DateTime? DataDevolucao { get; set; }

    // Navegação
    public virtual Livro? Livro { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
