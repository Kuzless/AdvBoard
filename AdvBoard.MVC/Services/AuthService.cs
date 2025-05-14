using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace AdvBoard.MVC.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly string _advApi = "/api/Auth/";
        private readonly IConfiguration _configuration;
        public AuthService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task Login(HttpContext context, AuthenticateResult result)
        {
            var email = result.Principal.FindFirstValue(ClaimTypes.Email);
            var name = result.Principal.FindFirstValue(ClaimTypes.Name);
            var userId = result.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _httpClient.PostAsJsonAsync(_configuration["APIUrl"] + _advApi, new
            {
                Email = email,
                Name = name,
                Id = userId
            });

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();

                context.Response.Cookies.Append("AccessToken", apiResponse, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None
                });
            }
            context.Session.SetInt32("Authorized", 1);
        }

        public void Logout(HttpContext context)
        {
            context.Session.Remove("Authorized");
            context.Response.Cookies.Delete("AccessToken");
        }
    }
}
