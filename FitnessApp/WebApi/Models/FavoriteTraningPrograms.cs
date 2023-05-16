using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class FavoriteTraningPrograms
    {
        public string Email { get; set; }
        public User User { get; set; }
        public ICollection<TraningProgram> TraningPrograms { get; set; }
    }
}
