using System.Net;
using System.Net.Mail;

namespace RunRun.Api.Services.v1
{
    public interface ISmtpClient
    {
        Task SendAsync(MailMessage mailMessage);
    }

    public class SmtpClient : ISmtpClient
    {
        private readonly IConfiguration _configuration;

        public SmtpClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendAsync(MailMessage mailMessage)
        {
            using (var client = new System.Net.Mail.SmtpClient(_configuration["smtpClient:host"]))
            {
                client.EnableSsl = true;
                client.Credentials =
                    new NetworkCredential(_configuration["smtpClient:userName"], _configuration["smtpClient:password"]);
                await client.SendMailAsync(mailMessage);
            }
        }
    }
}
