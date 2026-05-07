using System.Net;
using System.Net.Mail;

namespace AgendaAle.Worker.Services;

public class EmailService : IEmailService
{
    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var remetenteEmail = "avirmond2000@gmail.com";
        var remetenteSenha = "ykzd nssa ambd dcoq"; 

        using var client = new SmtpClient("smtp.gmail.com", 587)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(remetenteEmail, remetenteSenha)
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(remetenteEmail, "AgendaAle Notificações"),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        mailMessage.To.Add(toEmail);

        await client.SendMailAsync(mailMessage);
    }
}