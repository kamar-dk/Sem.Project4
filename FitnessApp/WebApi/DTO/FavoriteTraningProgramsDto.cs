using WebApi.Models;

namespace WebApi.DTO
{
    public class FavoriteTraningProgramsDto
    { 
        public string? Email { get; set; }
        public ICollection<TraningPrograms> TraningPrograms { get; set; }
    }
}
