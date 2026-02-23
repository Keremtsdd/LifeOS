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
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<UserAchievement> UserAchievements { get; set; }
        public DbSet<WeeklyGoal> WeeklyGoals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserActivity>()
                .HasOne(a => a.Category)
                .WithMany(c => c.UserActivities)
                .HasForeignKey(a => a.CategoryId);

            modelBuilder.Entity<WeeklyGoal>()
            .HasOne(g => g.Category)
            .WithMany()
            .HasForeignKey(g => g.CategoryId);

            base.OnModelCreating(modelBuilder);
        }
    }
}