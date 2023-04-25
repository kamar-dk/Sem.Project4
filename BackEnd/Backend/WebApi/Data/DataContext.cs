using Microsoft.EntityFrameworkCore;
using WebApi.Models.TraningTypes;
using WebApi.Models;
using WebApi.Data;
using WebApi.DTO;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Localization;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace WebApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        // Main Tables
        public DbSet<User> users { get; set; }
        public DbSet<UserData> userDatas { get; set; }
        public DbSet<TraningData> traningData { get; set; }
        public DbSet<FavoriteTraningPrograms> favoriteTraningPrograms { get; set; }
        public DbSet<TraningProgram> traningPrograms { get; set; }
        public DbSet<Server> server { get; set; }

        // TraningSessions
        public DbSet<RunningSession> runningSessions { get; set; } 
        public DbSet<BikeSession> bikeSessions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=sql.bsite.net\\MSSQL2016;Initial Catalog=kaspermartensen_Prj4;User ID=kaspermartensen_Prj4;Password=Bed2Fed2;Encrypt=False; Trust Server Certificate=False;Persist Security Info = True;");
            optionsBuilder.UseSqlServer("Data Source=localhost;User ID=sa;Password=<YourStrong@Passw0rd>;Initial Catalog=BED2;Encrypt=False; Trust Server Certificate=False;");
        }

              
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define keys
            modelBuilder.Entity<User>()
                .HasKey(u => u.Email);
            modelBuilder.Entity<UserData>()
                .HasKey(ud => ud.Email);
            modelBuilder.Entity<TraningData>()
                .HasKey(td => td.Email);
            modelBuilder.Entity<FavoriteTraningPrograms>()
                .HasKey(ftp => ftp.Email);
            modelBuilder.Entity<TraningProgram>()
                .HasKey(tp => tp.TraningProgramId);
            modelBuilder.Entity<RunningSession>()
                .HasKey(rs => rs.RunningSessionId);
            modelBuilder.Entity<BikeSession>()
                .HasKey(bs => bs.BikeSessionId);
            modelBuilder.Entity<Server>()
                .HasKey(s => s.ServerId);


            // Define User Relationships
            modelBuilder.Entity<User>()
                .HasOne(u => u.TraningData)
                .WithOne(td => td.User)
                .HasForeignKey<TraningData>(td => td.Email);
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserData)
                .WithOne(ud => ud.User)
                .HasForeignKey<UserData>(ud => ud.Email);
            modelBuilder.Entity<User>()
                .HasOne(u => u.FavoriteTraningPrograms)
                .WithOne(ftp => ftp.User)
                .HasForeignKey<FavoriteTraningPrograms>(ftp => ftp.Email);

            modelBuilder.Entity<User>()
                .HasOne(u => u.TraningData)
                .WithOne(td => td.User)
                .HasForeignKey<TraningData>(td => td.Email);

            modelBuilder.Entity<User>()
                .HasOne(u => u.UserData)
                .WithOne(ud => ud.User)
                .HasForeignKey<UserData>(ud => ud.Email);

            modelBuilder.Entity<User>()
                .HasOne(u => u.FavoriteTraningPrograms)
                .WithOne(ft => ft.User)
                .HasForeignKey<FavoriteTraningPrograms>(ft => ft.Email);

            modelBuilder.Entity<FavoriteTraningPrograms>()
                .HasMany(ft => ft.TraningPrograms);

            modelBuilder.Entity<Server>()
                .HasMany(s => s.Users);

            modelBuilder.Entity<Server>()
                .HasMany(s => s.TraningPrograms);


            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .IsRequired();  
        }
    }    
}
