using LifeOs.Context;
using LifeOs.DTOs;
using LifeOs.Entities;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("add-user")]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Kullanıcı/Bot başarıyla oluşturuldu!", userId = user.Id });
        }
    }
}