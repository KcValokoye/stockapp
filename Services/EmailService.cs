using DocumentFormat.OpenXml.Wordprocessing;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Cryptography;
using MimeKit.Text;
using StockAppSQ20.Dtos.Email;
using StockAppSQ20.Helpers;
using StockAppSQ20.Interfaces;

namespace StockAppSQ20.Services
{
    public class EmailService: IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService (IConfiguration config)

        {
            _config = config;
        }
        public async Task SendEmail(EmailDto request)
        {
            string body = PopulateRegisterEmail(request.UserName, request.Otp);
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(_config["EmailSettings:EmailUserName"])); //the email will be sent from EmailUserName in appsettings
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            //email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            var builder = new BodyBuilder();

            builder.HtmlBody = body;

            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();

            smtp.Connect(_config["EmailSettings:EmailHost"], int.Parse(_config["EmailSettings:EmailPort"]), SecureSocketOptions.StartTls); //connects to the host
            smtp.Authenticate(_config["EmailSettings:EmailUserName"], _config["EmailSettings:EmailPassword"]); //authenticates

            await smtp.SendAsync(email); //sends the email

            await smtp.DisconnectAsync(true);
        }

        private string PopulateRegisterEmail(string UserName, string Otp)
        {
            string body = string.Empty;

            string filePath = Directory.GetCurrentDirectory() + @"\Templates\RegistrationTemplate.html";

            using (StreamReader ReaderWriterLock = new StreamReader(filePath))
            {
                body = ReaderWriterLock.ReadToEnd();
            }

            body = body.Replace("{UserName}", UserName);
            body = body.Replace("{Otp}", Otp);

            return body;
        
        }
    }
}
