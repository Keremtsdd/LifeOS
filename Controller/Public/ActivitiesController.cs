using AutoMapper;
using LifeOs.DTOs;
using LifeOs.Context;
using LifeOs.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace LifeOs.Controller.Public
{

    [ApiController]
    [Route("api/[controller]")]
    public class ActivitiesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ActivityServices _services;
        private readonly IMapper _mapper;

        public ActivitiesController(AppDbContext context, ActivityServices services, IMapper mapper)
        {
            _context = context;
            _services = services;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateActivity([FromBody] ActivityCreateDto dto)
        {
            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                         ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "Kullanıcı kimliği doğrulanamadı. Lütfen tekrar giriş yapın." });
            }

            if (dto.DurationMinutes <= 0)
            {
                return BadRequest(new { message = "Süre 0'dan büyük olmalıdır." });
            }

            try
            {
                var earnedXP = await _services.CreateActivityAsync(dto, userId);

                return Ok(new
                {
                    message = "Aktivite buluta başarıyla kaydedildi!",
                    earnedXP = earnedXP,
                    assignedTo = userId
                });
            }
            catch (Exception ex)
            {
                var detail = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new { message = "Kayıt sırasında hata oluştu.", detail = detail });
            }
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _context.Categories
                .Where(c => !c.IsDeleted)
                .OrderBy(c => c.Name)
                .ToListAsync();

            var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
            return Ok(categoryDtos);
        }

        [HttpGet("my-activities")]
        public async Task<IActionResult> GetUserActivities()
        {
            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var activities = await _context.UserActivities
                 .Include(c => c.Category)
                 .Where(c => c.UserId == userId && !c.IsDeleted)
                 .OrderByDescending(c => c.CreatedDate)
                 .ToListAsync();

            var result = _mapper.Map<List<ActivityDto>>(activities);
            return Ok(result);
        }

        [HttpGet("daily-summary")]
        public async Task<IActionResult> GetDailySummary()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var today = DateTime.UtcNow.Date;

            var dailyActivity = await _context.UserActivities
                .Where(c => c.UserId == userId && c.CreatedDate >= today && !c.IsDeleted)
                .Include(c => c.Category)
                .ToListAsync();
            Console.WriteLine(userId);

            var summary = new
            {
                TotalXP = dailyActivity.Sum(a => a.EarnedXP),
                TotalMinutes = dailyActivity.Sum(a => a.DurationMinutes),
                ActivityCount = dailyActivity.Count,
                TopCategory = dailyActivity
                              .GroupBy(a => a.Category.Name)
                              .OrderByDescending(a => a.Count())
                              .Select(a => a.Key)
                              .FirstOrDefault() ?? "Henüz Yok"
            };
            return Ok(summary);

        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetUserStats()
        {
            var allStats = await _services.GetAllUsersStatsAsync();

            var Stats = allStats.FirstOrDefault(u => u.Id == 6);

            if (Stats == null) return NotFound("Kullanıcı istatistikleri bulunamadı.");

            return Ok(Stats);
        }

        [HttpGet("leaderboard")]
        public async Task<IActionResult> GetLeaderboard(int page = 1, int pageSize = 10)
        {
            var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var topUsers = await _context.Users
                .OrderByDescending(u => u.TotalXP)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new { u.FullName, u.Level, u.TotalXP })
                .ToListAsync();

            var currentUser = await _context.Users.FirstOrDefaultAsync(u => u.IdentityId == currentUserId);
            int myRank = 0;
            if (currentUser != null)
            {
                myRank = await _context.Users.CountAsync(u => u.TotalXP > currentUser.TotalXP) + 1;
            }

            return Ok(new
            {
                TopUsers = topUsers,
                MyRank = myRank,
                MyInfo = new { currentUser?.FullName, currentUser?.TotalXP, currentUser?.Level }
            });
        }

        [HttpGet("goals-progress")]
        public async Task<IActionResult> GetGoalsProgress()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var startOfWeek = DateTime.UtcNow.Date.AddDays(-(int)DateTime.UtcNow.DayOfWeek + (int)DayOfWeek.Monday);

            var goals = await _context.WeeklyGoals
                .Where(g => g.UserId == userId)
                .Include(g => g.Category)
                .Select(g => new
                {
                    CategoryName = g.Category.Name,
                    TargetMinutes = g.TargetMinutes,
                    CurrentMinutes = _context.UserActivities
                        .Where(a => a.UserId == userId && a.CategoryId == g.CategoryId && a.CreatedDate >= startOfWeek)
                        .Sum(a => a.DurationMinutes)
                })
                .ToListAsync();

            return Ok(goals);
        }

        [HttpGet("weekly-xp-chart")]
        public async Task<IActionResult> GetWeeklyXpChart()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var sevenDaysAgo = DateTime.UtcNow.Date.AddDays(-7);

            var activities = await _context.UserActivities
                .Where(a => a.UserId == userId && a.CreatedDate >= sevenDaysAgo)
                .ToListAsync();

            var chartData = activities
                .GroupBy(a => a.CreatedDate.Date)
                .Select(g => new
                {
                    Date = g.Key.ToString("dd/MM"),
                    DailyXP = g.Sum(a => a.EarnedXP)
                })
                .OrderBy(x => x.Date)
                .ToList();

            return Ok(chartData);
        }

        [Authorize]
        [HttpGet("stats-summary")]
        public async Task<IActionResult> GetStatsSummary()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                         ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var sevenDaysAgo = DateTime.UtcNow.Date.AddDays(-6);

            var activities = await _context.UserActivities
                .Where(a => a.UserId == userId && a.CreatedDate >= sevenDaysAgo && !a.IsDeleted)
                .Include(a => a.Category)
                .ToListAsync();

            var weeklyXp = activities
                .GroupBy(a => a.CreatedDate.Date)
                .Select(g => new { Day = g.Key.ToString("dd/MM"), TotalXP = g.Sum(a => a.EarnedXP) })
                .OrderBy(x => x.Day)
                .ToList();

            var categoryDistribution = activities
                .GroupBy(a => a.Category.Name)
                .Select(g => new { CategoryName = g.Key, TotalMinutes = g.Sum(a => a.DurationMinutes) })
                .ToList();

            return Ok(new { WeeklyXp = weeklyXp, CategoryDistribution = categoryDistribution });
        }

        [HttpPut("update-activity/{id}")]
        public async Task<IActionResult> UpdateActivity(int id, [FromBody] ActivityUpdateDto dto)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var activity = await _context.UserActivities
                .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);

            if (activity == null) return NotFound("Aktivite bulunamadı veya güncelleme yetkiniz yok.");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.IdentityId == userId);
            var category = await _context.Categories.FindAsync(dto.CategoryId);

            if (user != null && category != null)
            {
                user.TotalXP -= activity.EarnedXP;

                int newEarnedXp = (int)(dto.DurationMinutes * category.XPMultiplier);

                activity.CategoryId = dto.CategoryId;
                activity.DurationMinutes = dto.DurationMinutes;
                activity.EarnedXP = newEarnedXp;
                activity.Title = dto.Title;

                user.TotalXP += newEarnedXp;
            }

            await _context.SaveChangesAsync();
            return Ok(new { message = "Aktivite güncellendi!", newXP = activity.EarnedXP });
        }

        [Authorize]
        [HttpGet("user-profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                         ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.IdentityId == userId);

            if (user == null)
                return NotFound("Kullanıcı bulunamadı.");

            var totalXp = await _context.UserActivities
                .Where(a => a.UserId == userId && !a.IsDeleted)
                .SumAsync(a => a.EarnedXP);

            int level = (totalXp / 1000) + 1;
            int currentLevelXp = totalXp % 1000;
            double progress = currentLevelXp / 1000.0;

            return Ok(new
            {
                user.FullName,
                user.ProfilePictureUrl,   // 👈 BUNU EKLEDİK
                TotalXp = totalXp,
                Level = level,
                Progress = progress,
                CurrentLevelXp = currentLevelXp,
                NextLevelXp = 1000
            });
        }

        [HttpGet("achievements")]
        public async Task<IActionResult> GetAchievements()
        {
            var achievements = await _context.Achievements
                .OrderBy(a => a.RequirementValue)
                .Select(a => new
                {
                    a.Id,
                    a.Name,
                    a.Description,
                    a.IconUrl,
                    a.RequirementValue
                })
                .ToListAsync();
            return Ok(achievements);
        }
    }
}