using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Models
{
    
    public class UserData
    {
        public string Email { get; set; }
        public User User { get; set; }

        public float Weight { get; set; }
        public float Height { get; set; }
        public string Gender { get; set; }
        public DateTime DoB { get; set; }
    }
}
