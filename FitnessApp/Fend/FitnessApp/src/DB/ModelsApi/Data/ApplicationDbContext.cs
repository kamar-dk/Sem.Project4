using Microsoft.EntityFrameworkCore;
using ModelsApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelsApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options) { }

        public DbSet<EfAccount> Accounts { get; set; }
        public DbSet<EfJob> Jobs { get; set; }
        public DbSet<EfExpense> Expenses { get; set; }
        public DbSet<EfJobModel> JobModels { get; set; }
        public DbSet<EfModel> Models { get; set; }
        public DbSet<EfManager> Managers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure many-to-many relationship
            modelBuilder.Entity<EfJobModel>()
                .HasKey(p => new { p.EfJobId, p.EfModelId });

            modelBuilder.Entity<EfJobModel>()
                .HasOne(p => p.Job)
                .WithMany(p => p.JobModels)
                .HasForeignKey(pt => pt.EfJobId);

            modelBuilder.Entity<EfJobModel>()
                .HasOne(p => p.Model)
                .WithMany(p => p.JobModels)
                .HasForeignKey(p => p.EfModelId);

            // Configure indexes
            modelBuilder.Entity<EfAccount>()
                .HasIndex(p => p.Email)
                .IsUnique();
            modelBuilder.Entity<EfModel>()
                .HasIndex(p => p.Email)
                .IsUnique();
            modelBuilder.Entity<EfManager>()
                .HasIndex(p => p.Email)
                .IsUnique();
        }
    }
}
