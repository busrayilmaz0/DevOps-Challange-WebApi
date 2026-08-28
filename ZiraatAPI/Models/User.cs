using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ZiraatApi.Models
{
    [Table("Users")] // Veritabanındaki tablo adınız
    public class User
    {
        [Column("Id")]
        public int Id { get; set; }

        [JsonPropertyName("regName")]
        [Column("FullName")] // Veritabanındaki tam sütun adı
        public string FullName { get; set; } = string.Empty;

        [JsonPropertyName("regTc")]
        [Column("TCNo")] // PostgreSQL'deki büyük/küçük harf eşleşmesi
        public string TcNo { get; set; } = string.Empty;

        [JsonPropertyName("regPassword")]
        [Column("Password")]
        public string Password { get; set; } = string.Empty;

        [Column("IBAN")]
        public string IBAN { get; set; } = string.Empty;

        [Column("Balance")]
        public decimal Balance { get; set; }
    }
}