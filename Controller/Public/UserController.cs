using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LifeOs.Context;
using LifeOs.DTOs;
using System.Security.Claims;

namespace LifeOs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public UserController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpPost("update-profile-picture")]
        [RequestSizeLimit(10 * 1024 * 1024)]
        public async Task<IActionResult> UpdateProfilePicture([FromForm] UpdateProfilePictureDto model)
        {
            var identityId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                             ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(identityId))
                return Unauthorized();

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.IdentityId == identityId);

            if (user == null)
                return NotFound("Kullanıcı bulunamadı.");

            if (model.File == null || model.File.Length == 0)
                return BadRequest("Dosya yüklenmedi.");

            try
            {
                string webRootPath = _env.WebRootPath;
                if (string.IsNullOrEmpty(webRootPath))
                    webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

                var uploadsFolder = Path.Combine(webRootPath, "uploads", "profile_pics");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                // ✅ Eski resmi sil
                if (!string.IsNullOrEmpty(user.ProfilePictureUrl))
                {
                    var oldPath = Path.Combine(webRootPath, user.ProfilePictureUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }

                // ✅ Unique dosya adı
                var extension = Path.GetExtension(model.File.FileName) ?? ".jpg";
                var fileName = $"{identityId}_{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.File.CopyToAsync(stream);
                }

                user.ProfilePictureUrl = $"/uploads/profile_pics/{fileName}";
                await _context.SaveChangesAsync();

                return Ok(new { profilePictureUrl = user.ProfilePictureUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu hatası: {ex.Message}");
            }
        }
    }
}