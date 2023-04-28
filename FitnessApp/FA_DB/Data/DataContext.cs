using Microsoft.EntityFrameworkCore;
using FA_DB.Models;
using FA_DB.Models.TraningTypes;

namespace FA_DB.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
                : base(options) { }

        // Main Tables
        public DbSet<User> users { get; set; }
        public DbSet<UserData> userDatas { get; set; }
        public DbSet<TraningData> traningData { get; set; }
        public DbSet<FavoriteTraningPrograms> favoriteTraningPrograms { get; set; }
        public DbSet<TraningProgram> traningPrograms { get; set; }
        public DbSet<Server> server { get; set; }

        public DbSet<UserWeight> UserWeights { get; set; }
                
        // TraningSessions
        public DbSet<RunningSession> runningSessions { get; set; }
        public DbSet<BikeSession> bikeSessions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=sql.bsite.net\\MSSQL2016;Initial Catalog=kaspermartensen_Prj4;User ID=kaspermartensen_Prj4;Password=Bed2Fed2;Encrypt=False; Trust Server Certificate=False;Persist Security Info = True;");

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
                .HasKey(tp => tp.TraningProgramID);
            modelBuilder.Entity<RunningSession>()
                .HasKey(rs => rs.SessionID);
            modelBuilder.Entity<BikeSession>()
                .HasKey(bs => bs.SessionID);
            modelBuilder.Entity<Server>()
                .HasKey(s => s.ServerID);
            modelBuilder.Entity<UserWeight>()
                .HasKey(uw => uw.ID);


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

            modelBuilder.Entity<UserWeight>()
                .HasOne(w => w.UserData)
                .WithMany(uw => uw.UserWeights);

            modelBuilder.Entity<UserData>()
                .HasMany(uw => uw.UserWeights);


            modelBuilder.Entity<FavoriteTraningPrograms>()
                .HasMany(ft => ft.TraningPrograms);

            modelBuilder.Entity<Server>()
                .HasMany(s => s.Users);


            modelBuilder.Entity<Server>()
                .HasMany(s => s.TraningPrograms);
        }

    }
}
