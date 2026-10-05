

namespace Biblioteca.Application.Notifications;

public interface IEmailService
{
    Task EnviarEmailAsync( string destinatario,string assunto,string mensagem);
}
