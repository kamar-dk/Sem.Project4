using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class UserWeight
    {
        public int ID { get; set; } // PK
        public float Weight { get; set; }
        public DateTime date { get; set; }
        public UserData UserData { get; set; } // FK
    }
}
