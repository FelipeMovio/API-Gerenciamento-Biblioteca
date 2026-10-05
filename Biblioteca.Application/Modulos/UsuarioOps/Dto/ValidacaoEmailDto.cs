namespace Biblioteca.Application.Modules.UsuarioOps.Dtos;

public sealed record ValidacaoEmailDto(
    string Email,
    string CodigoHash,
    int TentativasIncorretas,
    DateTimeOffset DataExpiracao);