using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AdvBoard.Infrastructure;
using AdvBoard.Domain.Entities;

namespace AdvBoard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public AuthController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
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
            var claims = result.Principal?.Identities.FirstOrDefault()?.Claims;

            var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var name = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            if (email == null)
                return BadRequest("No email found from Google.");

            var db = HttpContext.RequestServices.GetService<DatabaseContext>();
            var existingUser = db.Users.FirstOrDefault(u => u.Email == email);

            if (existingUser == null)
            {
                db.Users.Add(new User { Email = email, UserName = email });
                await db.SaveChangesAsync();
            }

            return Redirect("https://localhost:7145/oauth-success");
        }
    }
}
