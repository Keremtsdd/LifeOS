using LifeOs.Context;
using LifeOs.DTOs;
using LifeOs.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LifeOs.Controller.Admin
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminActivitiesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdminActivitiesController(AppDbContext context)
        {
            _context = context;
        }


        [HttpPost("add-category")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryCreateDto dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Icon = dto.Icon,
                ColorHex = dto.ColorHex,
                XPMultiplier = dto.XPMultiplier,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Kategori başarıyla eklendi!", id = category.Id });
        }

        [HttpPost("add-achievement")]
        public async Task<IActionResult> AddAchievement([FromBody] AchievementCreateDto dto)
        {
            var newAchievement = new Achievement
            {
                Name = dto.Name,
                Description = dto.Description,
                IconUrl = dto.IconUrl,
                RequirementValue = dto.RequirementValue
            };
            _context.Achievements.Add(newAchievement);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Yeni rozet başarıyla sisteme eklendi!", id = newAchievement.Id });
        }

        [HttpGet("all-users-stats")]
        public async Task<IActionResult> GetAllUsersStats()
        {
            var sevenDaysAgo = DateTime.UtcNow.AddDays(-7);

            var users = await _context.Users
                .Select(u => new
                {
                    u.Id,
                    Email = u.Email ?? "Bilinmiyor",
                    TotalXP = _context.UserActivities
                        .Where(a => a.UserId == u.Id.ToString())
                        .Sum(a => (double?)a.EarnedXP) ?? 0,
                    LastActivityDate = _context.UserActivities
                        .Where(a => a.UserId == u.Id.ToString())
                        .OrderByDescending(a => a.CreatedDate)
                        .Select(a => (DateTime?)a.CreatedDate)
                        .FirstOrDefault(),
                    ActivityCount = _context.UserActivities
                        .Count(a => a.UserId == u.Id.ToString()),
                    WeeklyProgress = _context.UserActivities
                        .Where(a => a.UserId == u.Id.ToString() && a.CreatedDate >= sevenDaysAgo)
                        .GroupBy(a => a.CreatedDate.Date)
                        .Select(g => new
                        {
                            Date = g.Key,
                            DailyTotal = g.Sum(x => x.EarnedXP)
                        })
                        .ToList()
                })
                .OrderByDescending(u => u.TotalXP)
                .ToListAsync();

            var formattedResults = users.Select(u => new
            {
                u.Id,
                u.Email,
                u.TotalXP,
                u.ActivityCount,
                LastActivity = u.LastActivityDate == null ? "Hiç aktivite yok" : u.LastActivityDate.Value.ToString("dd/MM/yyyy HH:mm"),
                WeeklyChart = u.WeeklyProgress.Select(p => new
                {
                    Day = p.Date.ToString("dd/MM"),
                    XP = p.DailyTotal
                })
            });

            return Ok(formattedResults);
        }

        [HttpGet("system-summary")]
        public async Task<IActionResult> GetSystemSummary()
        {
            var today = DateTime.UtcNow.Date;

            var summary = new
            {
                TotalUsers = await _context.Users.CountAsync(),
                TotalActivitiesRecorded = await _context.UserActivities.CountAsync(),
                TotalXpDistributed = await _context.UserActivities.SumAsync(a => a.EarnedXP),
                ActivitiesToday = await _context.UserActivities.CountAsync(a => a.CreatedDate >= today),
                NewUsersThisWeek = await _context.Users.CountAsync(u => u.CreatedDate >= DateTime.UtcNow.AddDays(-7))
            };

            return Ok(summary);
        }

        [HttpGet("category-analytics")]
        public async Task<IActionResult> GetCategoryAnalytics()
        {
            var analytics = await _context.UserActivities
                .Include(a => a.Category)
                .GroupBy(a => new { a.CategoryId, CategoryName = a.Category.Name })
                .Select(g => new
                {
                    Category = g.Key.CategoryName,
                    TotalMinutes = g.Sum(x => x.DurationMinutes),
                    TotalActivities = g.Count(),
                    TotalXpEarned = g.Sum(x => x.EarnedXP)
                })
                .OrderByDescending(x => x.TotalMinutes)
                .ToListAsync();

            return Ok(analytics);
        }

        [HttpPatch("toggle-user-status/{userId}")]
        public async Task<IActionResult> ToggleUserStatus(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return NotFound("Kullanıcı bulunamadı.");

            user.IsActive = !user.IsActive;

            await _context.SaveChangesAsync();
            return Ok(new { message = $"Kullanıcı durumu güncellendi." });
        }

        [HttpDelete("delete-user/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return NotFound("Kullanıcı bulunamadı.");

            var userActivities = _context.UserActivities.Where(a => a.UserId == userId.ToString());
            _context.UserActivities.RemoveRange(userActivities);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"{user.Email} kullanıcısı ve tüm verileri kalıcı olarak silindi." });
        }

        [HttpPost("add-user")]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Kullanıcı/Bot başarıyla oluşturuldu!", userId = user.Id });
        }

    }
}