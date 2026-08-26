namespace ZiraatApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string TCNo { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string IBAN { get; set; } = string.Empty;
        public decimal Balance { get; set; }
    }
}