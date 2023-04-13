using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Server
    {
        public int ServerID { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<TraningProgram> TraningPrograms { get; set; }
    }
}
