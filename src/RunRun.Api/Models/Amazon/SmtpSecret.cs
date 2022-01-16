using System.Text.Json.Serialization;

namespace RunRun.Api.Models.Amazon
{
    public class SmtpSecret
    {
        [JsonPropertyName("userName")]
        public string UserName { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
