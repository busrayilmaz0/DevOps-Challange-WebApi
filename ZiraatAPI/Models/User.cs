using System.Text.Json.Serialization;

namespace ZiraatApi.Models
{
    public class User
    {
        public int Id { get; set; }

        [JsonPropertyName("regName")]
        public string FullName { get; set; } = string.Empty;

        [JsonPropertyName("regTc")]
        public string TcNo { get; set; } = string.Empty;

        [JsonPropertyName("regPassword")]
        public string Password { get; set; } = string.Empty;

        public string IBAN { get; set; } = string.Empty;

        public decimal Balance { get; set; }
    }
}