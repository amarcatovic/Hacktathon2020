using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System.Net.Mail;
using System.Threading.Tasks;
using Unjumble.Core.Helper;
using Unjumble.Core.Interfaces;

namespace Unjumble.Email
{
    public class EmailService : IEmailService
    {
        private readonly MimeMessage _mail;
        private readonly IOptions<EmailSettings> _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings;
            _mail = new MimeMessage();
            _mail.From.Add(new MailboxAddress(_emailSettings.Value.FromName, _emailSettings.Value.From));
        }

        public async Task<bool> SendEmailAsync(string to, string subject, string body, bool isHTML = false, string fullName = null)
        {
            _mail.To.Add(new MailboxAddress("Reciever", to));
            _mail.Subject = subject;


            if (isHTML)
                _mail.Body = new TextPart(TextFormat.Html) { Text = EmailHTML.ReturnEmailActivationHTML(body, fullName) };
            else
                _mail.Body = new TextPart("plain") { Text = body };


            try
            {
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 587, false);
                    await client.AuthenticateAsync(_emailSettings.Value.From, _emailSettings.Value.Password);
                    await client.SendAsync(_mail);
                    await client.DisconnectAsync(true);
                }

                return true;
            }
            catch (SmtpException exception)
            {
                System.Console.WriteLine(exception.Message);
                return false;
            }
        }
    }
}
