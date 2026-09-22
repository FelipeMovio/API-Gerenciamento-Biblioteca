using Biblioteca.Domain.Models.CategoriaMod;
using Biblioteca.Domain.Models.LivroMod;
using Biblioteca.Infrastructure.Modulos.CategoriaDb;
using Biblioteca.Infrastructure.Modulos.LivroDb;
using Biblioteca.Domain._Core.Base;
using FluentValidation;
using MediatR;

namespace Biblioteca.Application.Modules.LivroOps.Commands;

public sealed record CriarLivroCommand(
    string Titulo,
    string Autor,
    string ISBN,
    int AnoPublicacao,
    int CategoriaId)
    : IRequest<CriarLivroCommand.Response>
{
    public sealed class Handler(
        ILivroRepository livroRepository,
        ICategoriaRepository categoriaRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<CriarLivroCommand, Response>
    {
        public async Task<Response> Handle(
            CriarLivroCommand request,
            CancellationToken cancellationToken)
        {
            Categoria? categoria =
                await categoriaRepository.ObterPorIdAsync(
                    request.CategoriaId,
                    cancellationToken);

            if (categoria is null)
                throw new Exception(
                    $"A categoria com ID {request.CategoriaId} não foi encontrada.");

            string titulo = request.Titulo.Trim();
            string autor = request.Autor.Trim();
            string isbn = request.ISBN.Trim();

            var livro = new Livro
            {
                Titulo = titulo,
                Autor = autor,
                ISBN = isbn,
                AnoPublicacao = request.AnoPublicacao,
                CategoriaId = categoria.Id,
                Categoria = categoria,
                Disponivel = true
            };

            livroRepository.Adicionar(livro);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Response.FromEntity(livro);
        }
    }

    public sealed record Response(
        int Id,
        string Titulo,
        string Autor,
        string ISBN,
        int AnoPublicacao,
        int CategoriaId,
        bool Disponivel,
        CategoriaResponse Categoria)
    {
        public static Response FromEntity(Livro livro)
            => new(
                livro.Id,
                livro.Titulo,
                livro.Autor,
                livro.ISBN,
                livro.AnoPublicacao,
                livro.CategoriaId,
                livro.Disponivel,
                new CategoriaResponse(
                    livro.CategoriaId,
                    livro.Categoria?.Nome ?? string.Empty));
    }

    public sealed record CategoriaResponse(
        int Id,
        string Nome);

    public sealed class Validator
        : AbstractValidator<CriarLivroCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Titulo)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("O título do livro é obrigatório.")
                .MinimumLength(2)
                .WithMessage("O título deve ter pelo menos 2 caracteres.")
                .MaximumLength(200)
                .WithMessage("O título deve ter no máximo 200 caracteres.");

            RuleFor(x => x.Autor)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("O autor é obrigatório.")
                .MinimumLength(2)
                .WithMessage("O autor deve ter pelo menos 2 caracteres.")
                .MaximumLength(150)
                .WithMessage("O autor deve ter no máximo 150 caracteres.");

            RuleFor(x => x.ISBN)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("O ISBN é obrigatório.")
                .Length(10, 13)
                .WithMessage("O ISBN deve ter entre 10 e 13 caracteres.");

            RuleFor(x => x.AnoPublicacao)
                .InclusiveBetween(1000, 2100)
                .WithMessage(
                    "O ano de publicação deve estar entre 1000 e 2100.");

            RuleFor(x => x.CategoriaId)
                .GreaterThan(0)
                .WithMessage("A categoria é obrigatória.");
        }
    }
}