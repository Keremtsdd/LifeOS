using AutoMapper;
using LifeOs.DTOs;
using LifeOs.Context;
using LifeOs.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace LifeOs.Controller.Public
{
    [Authorize]
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
            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (dto.DurationMinutes <= 0)
                return BadRequest(new { message = "Süre 0'dan büyük olmalıdır." });

            try
            {
                var earnedXP = await _services.CreateActivityAsync(dto, userId);
                return Ok(new
                {
                    message = "Aktivite buluta başarıyla kaydedildi!",
                    earnedXP = earnedXP
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Kayıt sırasında hata oluştu.", detail = ex.Message });
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

        [HttpGet("myactivities")]
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
    }
}