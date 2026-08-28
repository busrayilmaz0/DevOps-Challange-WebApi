using System.Text.Json.Serialization;

namespace ZiraatApi.Models
{
    public class User
    {
        public int Id { get; set; }

        [JsonPropertyName("fullName")]
        public string FullName { get; set; } = string.Empty;

        [JsonPropertyName("tc")]
        public string TCNo { get; set; } = string.Empty;

        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;

        [JsonPropertyName("iban")]
        public string IBAN { get; set; } = string.Empty;

        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
    }
}