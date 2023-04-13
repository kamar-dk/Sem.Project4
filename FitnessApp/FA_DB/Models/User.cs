using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FA_DB.Models
{
    
    public class User
    {
        public string Email { get; set; }
        //public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] Salt { get; set; }

        public TraningData TraningData { get; set; }
        public UserData UserData { get; set; }
        public FavoriteTraningPrograms FavoriteTraningPrograms { get; set; }
    }
}
