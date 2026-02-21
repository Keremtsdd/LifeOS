using LifeOs.Entities;
using Microsoft.EntityFrameworkCore;

namespace LifeOs.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserActivity>()
            .HasOne(a => a.Category)
            .WithMany(c => c.UserActivities)
            .HasForeignKey(a => a.CategoryId);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Physical", Icon = "fitness", ColorHex = "#FF4B2B", XPMultiplier = 1.2 },
                new Category { Id = 2, Name = "Learning", Icon = "school", ColorHex = "#AF40FF", XPMultiplier = 1.5 },
                new Category { Id = 3, Name = "Work", Icon = "work", ColorHex = "#2196F3", XPMultiplier = 1.0 },
                new Category { Id = 4, Name = "Social", Icon = "groups", ColorHex = "#4CAF50", XPMultiplier = 1.1 },
                new Category { Id = 5, Name = "Mental", Icon = "self-improvement", ColorHex = "#FFC107", XPMultiplier = 1.3 },
                new Category { Id = 6, Name = "Creative", Icon = "palette", ColorHex = "#E91E63", XPMultiplier = 1.4 }
            );


            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    IdentityId = "26867e6c-bb13-4843-a261-8d0e03e0d038",
                    FullName = "Kerem Taşdemir",
                    Level = 1,
                    TotalXP = 0,
                    CurrentLevelXP = 0,
                    NextLevelXP = 1000
                },
                new User { Id = 3, IdentityId = "bot-002", FullName = "Zeynep Kaya", Level = 3, TotalXP = 2500, NextLevelXP = 3000 },
                new User { Id = 4, IdentityId = "bot-003", FullName = "Mert Demir", Level = 1, TotalXP = 300, NextLevelXP = 1000 },
                new User { Id = 5, IdentityId = "bot-004", FullName = "Selin Aydın", Level = 2, TotalXP = 1200, NextLevelXP = 2000 }
            );

            base.OnModelCreating(modelBuilder);

        }
    }
}
