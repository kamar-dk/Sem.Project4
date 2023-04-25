namespace WebApi.DTO
{
    public class UserDto
    {
        public string Email { get; set; }
        //public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] PasswordHash { get; set; }
    }
}
