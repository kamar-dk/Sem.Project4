using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Models;
//using WebApi.Models.TraningTypes;
using Microsoft.EntityFrameworkCore.ValueGeneration;


namespace WebApi.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        // Main Tables
        public DbSet<User> users { get; set; }
        public DbSet<UserData> userDatas { get; set; }
        public DbSet<TrainingData> traningData { get; set; }
        public DbSet<FavoriteTraningPrograms> favoriteTraningPrograms { get; set; }
        public DbSet<TraningPrograms> traningPrograms { get; set; }
        public DbSet<UserWeight> UserWeights { get; set; }

        // TraningSessions
        //public DbSet<RunningSession> runningSessions { get; set; }
        //public DbSet<BikeSession> bikeSessions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=sql.bsite.net\\MSSQL2016;Initial Catalog=kaspermartensen_Prj4nyny;User ID=kaspermartensen_Prj4nyny;Password=123456;Encrypt=False; Trust Server Certificate=False;Persist Security Info = True;");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // Define User
            modelBuilder.Entity<User>()
                .HasKey(u => u.Email);
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserData)
                .WithOne(ud => ud.User)
                .HasForeignKey<UserData>(ud => ud.Email);
            modelBuilder.Entity<User>()
                .HasMany(u => u.FavoriteTraningPrograms)
                .WithOne(ftp => ftp.User)
                .HasForeignKey(ftp => ftp.Email);
            modelBuilder.Entity<User>()
                .HasMany(u => u.TraningDatas);

            //Define UserData
            modelBuilder.Entity<UserData>()
                .HasKey(ud => ud.Email);
            modelBuilder.Entity<UserData>()
                .HasMany(uw => uw.UserWeights);

            // Define UserWeight
            modelBuilder.Entity<UserWeight>()
                .HasKey(uw => uw.ID);
            modelBuilder.Entity<UserWeight>()
                .HasOne(w => w.UserData)
                .WithMany(uw => uw.UserWeights);

            // Define TraningData
            modelBuilder.Entity<TrainingData>()
                .HasKey(td => td.Id);
            modelBuilder.Entity<TrainingData>()
                .HasOne(u => u.User)
                .WithMany(td => td.TraningDatas)
                .HasForeignKey(td => td.UserId);

            // Define FavoriteTraningPrograms
            modelBuilder.Entity<FavoriteTraningPrograms>()
                .HasKey(ftp => ftp.FavoriteTraningProgramsID);
            modelBuilder.Entity<FavoriteTraningPrograms>()
                .HasIndex(ftp => ftp.FavoriteTraningProgramsID)
                .IsUnique();
            modelBuilder.Entity<FavoriteTraningPrograms>()
                .HasOne(ftp => ftp.TraningProgram)
                .WithMany(tp => tp.FavoriteTraningPrograms)
                .HasForeignKey(ftp => ftp.TraningProgramID);
            modelBuilder.Entity<FavoriteTraningPrograms>()
                .HasOne(u => u.User)
                .WithMany(ftp => ftp.FavoriteTraningPrograms)
                .HasForeignKey(ftp => ftp.Email);

            // Define TraningPrograms
            modelBuilder.Entity<TraningPrograms>()
                .HasKey(tp => tp.TraningProgramID);
            


            //modelBuilder.Entity<FavoriteTraningPrograms>()
            //    .HasOne(tp => tp.TraningProgram)
            //    .WithMany(ftp => ftp.FavoriteTraningPrograms)
            //    .HasForeignKey(ftp => ftp.TraningProgramID);

            //modelBuilder.Entity<TraningPrograms>()
            //    .HasMany(tp => tp.FavoriteTraningPrograms)
            //    .WithOne(ftp => ftp.TraningProgram)
            //    .HasForeignKey(ftp => ftp.TraningProgramID)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
