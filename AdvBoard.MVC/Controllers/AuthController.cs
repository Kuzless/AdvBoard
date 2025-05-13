using Microsoft.AspNetCore.Mvc;
using AdvBoard.MVC.Services.Static;

namespace AdvBoard.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public AuthController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return Redirect($"{_configuration["APIUrl"]}/api/Auth/google-login");
        }
        public IActionResult Logout()
        {
            AuthService.Logout(HttpContext, _httpClient);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Callback(string token)
        {
            var isSuccessful = AuthService.Login(HttpContext, _httpClient, token);
            if (isSuccessful) {
                return RedirectToAction("Index", "Announcement");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
