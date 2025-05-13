namespace AdvBoard.MVC.Services.Static
{
    public static class AuthService
    {
        public static bool Login(HttpContext context, HttpClient httpClient, string token)
        {
            if (token == null)
            {
                return false;
            }
            string cookie = context.Request.Headers.Cookie!;
            context.Session.SetString("AccessToken", token);
            context.Session.SetString("Cookie", cookie);
            context.Session.SetInt32("Authorized", 1);
            return true;
        }

        public static void Logout(HttpContext context, HttpClient httpClient)
        {
            context.Session.Remove("AccessToken");
            context.Session.Remove("Cookie");
            context.Session.Remove("Authorized");
        }
    }
}
