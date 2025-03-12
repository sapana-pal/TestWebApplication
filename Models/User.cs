namespace TestWebApplication.Models
{
    public class User
    {
        public int Id { get; set; }  // Primary Key

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }  // Store hashed passwords for security

        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Address { get; set; }

        public bool IsActive { get; set; }  // To manage user status

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }

}
