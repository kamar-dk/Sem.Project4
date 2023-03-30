using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA_DB.Models
{
    public class FavoriteTraningPrograms
    {
        public string Email { get; set; }
        public User User { get; set; }
        public List<TraningProgram>? TraningPrograms { get; set; }
    }
}
