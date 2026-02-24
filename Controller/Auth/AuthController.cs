using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using LifeOs.DTOs;

namespace LifeOs.Controller.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly string _supabaseUrl = "https://pbzpgepwwlgsstnmopjb.supabase.co/auth/v1/token?grant_type=password";
        private readonly string _supabaseAnonKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InBienBnZXB3d2xnc3N0bm1vcGpiIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NzE0OTcyNDcsImV4cCI6MjA4NzA3MzI0N30.3Y7ilRPIsJDn5KjL9k6Ow1zsh_1TFEA77CPbg4EOXX4";

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var requestBody = new { email = dto.Email, password = dto.Password };
            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("apikey", _supabaseAnonKey);

            var response = await _httpClient.PostAsync(_supabaseUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<JsonElement>(responseString);
                var token = result.GetProperty("access_token").GetString();

                return Ok(new { Token = token });
            }

            return Unauthorized(new { message = "E-posta veya şifre hatalı!" });
        }
    }
}