using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA_DB.Models
{
    public class Server
    {
        public ICollection<User> Users { get; set; }
        public ICollection<TraningProgram> TraningPrograms { get; set; }
    }
}
