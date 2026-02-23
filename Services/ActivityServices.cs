using LifeOs.DTOs;
using LifeOs.Entities;
using LifeOs.Interfaces;
using LifeOs.Context;
using Microsoft.EntityFrameworkCore;

namespace LifeOs.Services
{
    public class ActivityServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;

        public ActivityServices(IUnitOfWork unitOfWork, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<int> CreateActivityAsync(ActivityCreateDto dto, string userId)
        {
            var category = await _context.Categories.FindAsync(dto.CategoryId);
            if (category == null) throw new Exception("Kategori Bulunamadı!");

            int earnedXp = (int)(dto.DurationMinutes * category.XPMultiplier);

            var activity = new UserActivity
            {
                UserId = userId,
                Title = dto.Title,
                DurationMinutes = dto.DurationMinutes,
                CategoryId = dto.CategoryId,
                EarnedXP = earnedXp,
                CreatedDate = DateTime.UtcNow
            };

            await _context.UserActivities.AddAsync(activity);
            await UpdateUserProgress(userId, earnedXp);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.IdentityId == userId);
            if (user != null)
            {
                await CheckAndAwardAchievements(userId, user.TotalXP);
            }

            await _unitOfWork.CommitAsync();
            return earnedXp;
        }

        private async Task UpdateUserProgress(string userId, int earnedXp)
        {
            var user = await _context.Users.FirstOrDefaultAsync(c => c.IdentityId == userId);
            if (user == null) return;

            user.TotalXP += earnedXp;
            user.CurrentLevelXP += earnedXp;

            while (user.CurrentLevelXP > user.NextLevelXP)
            {
                user.CurrentLevelXP -= user.NextLevelXP;
                user.Level++;

                user.NextLevelXP = (int)(user.NextLevelXP * 1.2);
            }
        }

        private async Task CheckAndAwardAchievements(string userId, int totalXp)
        {
            var ownedAchievementIds = await _context.UserAchievements
                .Where(ua => ua.UserId == userId)
                .Select(ua => ua.AchievementId)
                .ToListAsync();

            var newAchievements = await _context.Achievements
                .Where(a => !ownedAchievementIds.Contains(a.Id) && totalXp >= a.RequirementValue)
                .ToListAsync();

            foreach (var achievement in newAchievements)
            {
                _context.UserAchievements.Add(new UserAchievement
                {
                    UserId = userId,
                    AchievementId = achievement.Id,
                    EarnedDate = DateTime.UtcNow
                });
            }
        }

        public async Task<bool> ProgressLevelUp(User user)
        {
            while (user.TotalXP >= user.NextLevelXP)
            {
                user.Level++;
                user.CurrentLevelXP = user.TotalXP - user.NextLevelXP;
                user.NextLevelXP = (int)(user.NextLevelXP * 1.2);
            }
            return true;
        }

    }
}