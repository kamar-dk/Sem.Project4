using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{

    public class User
    {
        public User()
        {
            TraningDatas = new HashSet<TraningData>();
        }
        public string? Email { get; set; }
        //public string Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? Gender { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? Salt { get; set; }

        //public TraningData TraningData { get; set; }
        public UserData UserData { get; set; }
        public ICollection<FavoriteTraningPrograms> FavoriteTraningPrograms { get; set; }

        public virtual ICollection<TraningData> TraningDatas { get; set; } = new List<TraningData>();
    }
}
