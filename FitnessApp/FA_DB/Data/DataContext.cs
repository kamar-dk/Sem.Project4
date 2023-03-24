using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FA_DB.Models;


namespace FA_DB.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> users { get; set; } = default;
        public DbSet<UserData> userDatas { get; set; } = default;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            
        }

    }
}
