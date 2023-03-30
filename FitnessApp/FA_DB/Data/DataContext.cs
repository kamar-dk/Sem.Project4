using Microsoft.EntityFrameworkCore;
using FA_DB.Models;
using FA_DB.Models.TraningTypes;

namespace FA_DB.Data
{
    public class DataContext : DbContext
    {
        // Main Tables
        public DbSet<User> users { get; set; }
        public DbSet<UserData> userDatas { get; set; }
        public DbSet<TraningData> trantingData { get; set; }
        
        // TraningSessions
        public DbSet<RunningSession> runningSessions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=sql.bsite.net\\MSSQL2016;Initial Catalog=kaspermartensen_Prj4;User ID=kaspermartensen_Prj4;Password=Bed2Fed2;Encrypt=False; Trust Server Certificate=False;Persist Security Info = True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.TraningData)
                .WithOne(td => td.User)
                .HasForeignKey<TraningData>(td => td.Email);

            modelBuilder.Entity<User>()
                .HasOne(u => u.UserData)
                .WithOne(ud => ud.User)
                .HasForeignKey<UserData>(ud => ud.Email);
            
        }

    }
}
