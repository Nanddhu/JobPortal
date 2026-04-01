using JobPortal.Application.Interfaces;
using MailKit;
using MimeKit;
using MailKit.Net.Smtp;


namespace JobPortal.Application.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse("nandhinivallal@gmail.com"));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;

            email.Body = new TextPart("plain")
            {
                Text = body
            };

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync("smtp.gmail.com", 587, false);

            await smtp.AuthenticateAsync("nandhinivallal@gmail.com", "ufzd drpi njjm uvyu");

            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
