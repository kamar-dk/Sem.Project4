using FA_DB.Models;

namespace WebApi.DTO
{
    public class UserWeightDto
    {
        public float Weight { get; set; }
        public DateTime Date { get; set; }

        public UserData? UserData { get; set; }
    }

}
