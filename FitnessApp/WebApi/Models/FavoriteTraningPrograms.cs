using Castle.Components.DictionaryAdapter;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Models
{

    public class FavoriteTraningPrograms
    {

        public int FavoriteTraningProgramsID { get; set; }
        
        public int TraningProgramID { get; set; } 
        public TraningPrograms TraningProgram { get; set; }
       
        public string Email { get; set; }
        public User User { get; set; }
        public ICollection<TraningPrograms> TraningPrograms { get; set; }
        public string Name { get; internal set; }
    }
}
