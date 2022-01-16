using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using RunRun.Api.Models.Amazon;
using System.Text.Json;

namespace RunRun.Api.Services.v1
{
    public interface ISecretManager
    {
        Task<SmtpSecret> GetSmtpSecret();
    }

    public class SecretManager : ISecretManager
    {
        private readonly AmazonSecretsManagerConfig _config;
        public SecretManager()
        {
            _config = new AmazonSecretsManagerConfig { RegionEndpoint = RegionEndpoint.EUWest1 };
        }

        private const string SecretId = "arn:aws:secretsmanager:eu-west-1:632122663289:secret:smtp-g71TUu";

        public async Task<SmtpSecret> GetSmtpSecret()
        {
            using var client = new AmazonSecretsManagerClient(_config);

            var request = new GetSecretValueRequest
            {
                SecretId = SecretId
            };

            var secretString  = (await client.GetSecretValueAsync(request)).SecretString;
            return JsonSerializer.Deserialize<SmtpSecret>(secretString);
        }
    }
}
