using AdvBoard.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AdvBoard.Infrastructure.Configuration
{
    public class CustomUserStore : UserStore<User>
    {
        public CustomUserStore(DatabaseContext context) : base(context)
        {
            AutoSaveChanges = false;
        }
    }
}
