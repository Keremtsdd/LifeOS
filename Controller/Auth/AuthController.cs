using System.Text;
using System.Text.Json;
using LifeOs.Context;
using LifeOs.DTOs;
using LifeOs.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LifeOs.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly AppDbContext _context;
        private readonly string _supabaseUrl = "https://pbzpgepwwlgsstnmopjb.supabase.co/auth/v1";
        private readonly string _supabaseAnonKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InBienBnZXB3d2xnc3N0bm1vcGpiIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NzE0OTcyNDcsImV4cCI6MjA4NzA3MzI0N30.3Y7ilRPIsJDn5KjL9k6Ow1zsh_1TFEA77CPbg4EOXX4";

        public AuthController(IHttpClientFactory httpClientFactory, AppDbContext context)
        {
            _httpClient = httpClientFactory.CreateClient();
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var signupBody = new { email = dto.Email, password = dto.Password };
            var content = new StringContent(JsonSerializer.Serialize(signupBody), Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("apikey", _supabaseAnonKey);

            var response = await _httpClient.PostAsync($"{_supabaseUrl}/signup", content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest(new { message = "Supabase kayıt hatası", detail = responseString });
            }

            try
            {
                using var doc = JsonDocument.Parse(responseString);
                var root = doc.RootElement;

                if (root.TryGetProperty("user", out JsonElement userElement) &&
                    userElement.TryGetProperty("id", out JsonElement idElement))
                {
                    var supabaseId = idElement.GetString();

                    var newUser = new User
                    {
                        IdentityId = supabaseId,
                        FullName = dto.FullName,
                        Email = dto.Email,
                        Level = 1,
                        TotalXP = 0,
                        CurrentLevelXP = 0,
                        NextLevelXP = 1000,
                        ProfilePictureUrl = null
                    };

                    _context.Users.Add(newUser);
                    await _context.SaveChangesAsync();

                    return Ok(new { message = "Kayıt başarılı, hoş geldin!", userId = supabaseId });
                }
                else
                {
                    return BadRequest(new { message = "Supabase başarı döndü ancak kullanıcı bilgisi alınamadı.", detail = responseString });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "JSON ayrıştırma veya veritabanı hatası", detail = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var requestBody = new { email = dto.Email, password = dto.Password };
            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("apikey", _supabaseAnonKey);

            var response = await _httpClient.PostAsync($"{_supabaseUrl}/token?grant_type=password", content);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<JsonElement>(responseString);
                var token = result.GetProperty("access_token").GetString();

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

                return Ok(new
                {
                    Token = token,
                    FullName = user?.FullName,
                    Level = user?.Level
                });
            }
            return Unauthorized(new { message = "E-posta veya şifre hatalı!" });
        }

    }
}