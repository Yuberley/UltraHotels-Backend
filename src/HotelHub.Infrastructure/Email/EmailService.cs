using System.Net.Mail;
using HotelHub.Application.Abstractions.Email;

namespace HotelHub.Infrastructure.Email;

internal sealed class EmailService : IEmailService
{
    // TODO: Change the email origin and password to environment variables.
    private const string EmailOrigin = "yeferson.guerrero2613@alumnos.udg.mx";
    private const string EmailPassword = "llhh wwop tore melg";
    
    public Task SendAsync(Domain.SharedValueObjects.Email recipient, string subject, string body)
    {
        MailMessage mail = new MailMessage(EmailOrigin, recipient.Value, subject, body);
        SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new System.Net.NetworkCredential(EmailOrigin, EmailPassword),
            EnableSsl = true
        };
        client.Send(mail);
        return Task.CompletedTask;
    }
}