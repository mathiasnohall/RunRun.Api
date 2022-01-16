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
        private readonly ISecretManager _secretManager;

        public SmtpClient(IConfiguration configuration, ISecretManager secretManager)
        {
            _configuration = configuration;
            _secretManager = secretManager;
        }

        public async Task SendAsync(MailMessage mailMessage)
        {
            var secret = await _secretManager.GetSmtpSecret();
            using (var client = new System.Net.Mail.SmtpClient(_configuration["smtpClient:host"]))
            {
                client.EnableSsl = true;
                client.Port = 587;
                client.Credentials =
                    new NetworkCredential(secret.UserName, secret.Password);
                await client.SendMailAsync(mailMessage);
            }
        }
    }
}
