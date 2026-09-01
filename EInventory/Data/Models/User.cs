namespace Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        // For demo only: store password as plain text. Replace with hashing for production.
        public string Password { get; set; } = string.Empty;
    }
}
