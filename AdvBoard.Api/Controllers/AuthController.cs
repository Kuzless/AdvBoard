using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using AdvBoard.Application.Interfaces;

namespace AdvBoard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        [HttpGet("google-login")]
        public IActionResult Login()
        {
            var properties = new AuthenticationProperties()
            {
                RedirectUri = Url.Action("Callback")
            };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("google-callback")]
        public async Task<IActionResult> Callback()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            var token = await _authService.Authenticate(result);
            return Redirect($"{_configuration["FrontendUrl"]}/Auth/Callback?token={token}");
        }
    }
}
