using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using AdvBoard.MVC.Services;

namespace AdvBoard.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("Callback")
            };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet]
        public async Task<IActionResult> Callback()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            await _authService.Login(HttpContext, result);
            return RedirectToAction("Index", "Announcement");
        }
        public IActionResult Logout()
        {
            _authService.Logout(HttpContext);
            return RedirectToAction("Index", "Home");
        }
    }
}
