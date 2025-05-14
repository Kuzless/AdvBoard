using Microsoft.AspNetCore.Mvc;

namespace AdvBoard.MVC.Guard
{
    public class AuthAttribute : TypeFilterAttribute
    {
        public AuthAttribute() : base(typeof(AuthFilter)) { }
    }
}
