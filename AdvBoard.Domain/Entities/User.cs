using Microsoft.AspNetCore.Identity;

namespace AdvBoard.Domain.Entities
{
    public class User : IdentityUser 
    {
        public List<Announcement> Announcements { get; set; }
        public string Name { get; set; }
        public string AccessToken { get; set; }
    }
}
