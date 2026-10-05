using Biblioteca.Domain.Models.LivroMod;
using Biblioteca.Domain.Models.UsuarioMod;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Domain.Models.EmprestimoMod;

public class Emprestimo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    [Range(1, int.MaxValue)]
    public int LivroId { get; private set; }

    [Range(1, int.MaxValue)]
    public int UsuarioId { get; private set; }

    public DateTime DataEmprestimo { get; private set; }

    public DateTime DataDevolucaoPrevista { get; private set; }

    public DateTime? DataDevolucao { get; private set; }

    // Navegação
    public virtual Livro? Livro { get; private set; }

    public virtual Usuario? Usuario { get; private set; }

    // Construtor utilizado pelo Entity Framework
    private Emprestimo() { }

    // Construtor para criação pela aplicação
    public Emprestimo(
        int livroId,
        int usuarioId,
        DateTime dataEmprestimo,
        DateTime dataDevolucaoPrevista)
    {
        if (livroId <= 0)
            throw new ArgumentException("O ID do livro deve ser maior que zero.");

        if (usuarioId <= 0)
            throw new ArgumentException("O ID do usuário deve ser maior que zero.");

        if (dataDevolucaoPrevista <= dataEmprestimo)
            throw new ArgumentException(
                "A data prevista de devolução deve ser posterior à data do empréstimo.");

        LivroId = livroId;
        UsuarioId = usuarioId;
        DataEmprestimo = dataEmprestimo;
        DataDevolucaoPrevista = dataDevolucaoPrevista;
    }

    public void RegistrarDevolucao(DateTime dataDevolucao)
    {
        if (DataDevolucao.HasValue)
            throw new InvalidOperationException(
                "Este empréstimo já foi devolvido.");

        if (dataDevolucao < DataEmprestimo)
            throw new ArgumentException(
                "A data de devolução não pode ser anterior à data do empréstimo.");

        DataDevolucao = dataDevolucao;
    }

    public bool EstaDevolvido()
    {
        return DataDevolucao.HasValue;
    }
}