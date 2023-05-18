namespace WebApi.DTO
{
    public class UserDto
    {        
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Gender { get; set; } = "";
        public byte[]? PasswordHash { get; set; }

        public byte[]? Salt { get; set; }

        public string Token { get; set; }


        //public virtual List<TraningDatasDto> TraningDatasDtos { get; set; } = new List<TraningDatasDto>();
        //public virtual ICollection<TraningDatasDto> TraningDatasDtos { get; set; } = new List<TraningDatasDto>();
    }
}
