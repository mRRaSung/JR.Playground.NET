using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Threading.Tasks;

namespace Mail
{
    interface IMailer
    {
        Task SendEmalAsync(string email, string subject, string body);
    }

    public class Mailer : IMailer
    {
        public async Task SendEmalAsync(string email, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Sender", "account@gmail.com"));
            message.To.Add(new MailboxAddress("Receiver", email));
            message.Subject = subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

            using var client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

            // Note: only needed if the SMTP server requires authentication
            await client.AuthenticateAsync("account@gmail.com", "password");
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
