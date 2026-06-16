using System;
using System.Threading.Tasks;
using ServiceAbstraction;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration; 

namespace Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string to, string subject, string htmlBody)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("HospitalSystem", _configuration.GetSection("EmailSettings")["Email"]!));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = htmlBody };
            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_configuration.GetSection("EmailSettings")["Host"]!, int.Parse(_configuration.GetSection("EmailSettings")["Port"]!), MailKit.Security.SecureSocketOptions.StartTls);

                await client.AuthenticateAsync(_configuration.GetSection("EmailSettings")["Email"]!, _configuration.GetSection("EmailSettings")["Password"]!);
                await client.SendAsync(message);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                throw;
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }
    }
}