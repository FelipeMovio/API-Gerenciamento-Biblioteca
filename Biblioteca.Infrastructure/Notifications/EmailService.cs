using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Biblioteca.Application.Notifications;

namespace Biblioteca.Infrastructure.Notifications;

public class EmailService(IConfiguration configuration) : IEmailService
{
    public async Task EnviarEmailAsync(
        string destinatario,
        string assunto,
        string mensagem)
    {
        var emailRemetente =
            configuration["Email:Address"];

        var senhaEmail =
            configuration["Email:Password"];

        var email = new MimeMessage();

        email.From.Add(
            new MailboxAddress(
                "Biblioteca API",
                emailRemetente));

        email.To.Add(
            MailboxAddress.Parse(destinatario));

        email.Subject = assunto;

        email.Body = new TextPart("plain")
        {
            Text = mensagem
        };

        using var smtp = new SmtpClient();

        await smtp.ConnectAsync(
            "smtp.gmail.com",
            587,
            MailKit.Security.SecureSocketOptions.StartTls);

        await smtp.AuthenticateAsync(
            emailRemetente,
            senhaEmail);

        await smtp.SendAsync(email);

        await smtp.DisconnectAsync(true);
    }
}