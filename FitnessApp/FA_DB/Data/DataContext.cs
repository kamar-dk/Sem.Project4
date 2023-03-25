using Microsoft.EntityFrameworkCore;
using FA_DB.Models;
using FA_DB.Models.TraningTypes;

namespace FA_DB.Data
{
    public class DataContext : DbContext
    {
        // Main Tables
        public DbSet<User> users { get; set; } = default;
        public DbSet<UserData> userDatas { get; set; } = default;
        public DbSet<TraningData> trantingData { get; set; } = default;
        
        // TraningSessions
        public DbSet<RunningSession> runningSessions { get; set; } = default;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=sql.bsite.net\\MSSQL2016;Initial Catalog=kaspermartensen_Prj4;User ID=kaspermartensen_Prj4;Password=Bed2Fed2;Encrypt=False; Trust Server Certificate=False;Persist Security Info = True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            
        }

    }
}
