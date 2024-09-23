namespace Udemy.DotNetWebAPI.Models.DTO
{
    public class CreateUserDTO
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Intro { get; set; }
        public string Profile { get; set; }
    }
}
