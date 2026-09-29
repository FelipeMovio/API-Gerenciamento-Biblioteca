namespace Biblioteca.Application.Security;

public interface IJwtService
{
    string GerarToken(
        int usuarioId,
        string nome,
        string email);
}