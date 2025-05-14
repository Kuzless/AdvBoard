using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AdvBoard.MVC.Guard
{
    public class AuthFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Cookies["AccessToken"];
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new RedirectToActionResult("Logout", "Auth", null);
            }
        }
    }
}
