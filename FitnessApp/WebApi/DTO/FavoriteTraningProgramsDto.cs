using WebApi.Models;

namespace WebApi.DTO
{
    public class FavoriteTraningProgramsDto
    {
        public int FavoriteTraningProgramsID { get; set; }
        public string? Email { get; set; }
        public int TraningProgramID { get; set; }

        //public ICollection<TraningPrograms> TraningPrograms { get; set; }
    }
}
