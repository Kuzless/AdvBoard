using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace AdvBoard.MVC.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task Login(HttpContext context, AuthenticateResult result)
        {
            var email = result.Principal.FindFirstValue(ClaimTypes.Email);
            var name = result.Principal.FindFirstValue(ClaimTypes.Name);
            var userId = result.Principal.FindFirstValue(ClaimTypes.NameIdentifier);

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7007/api/Auth/", new
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
        }
    }
}
