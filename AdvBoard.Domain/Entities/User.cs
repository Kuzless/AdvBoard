using Microsoft.AspNetCore.Identity;

namespace AdvBoard.Domain.Entities
{
    public class User : IdentityUser 
    {
        public List<Announcement> Announcements { get; set; }
    }
}
