namespace Domain.Entities
{
    public class User : EntityBase
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public Role Role { get; set; }

        public UserStatus Status { get; set; }
    }
}