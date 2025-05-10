using AdvBoard.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace AdvBoard.Infrastructure
{
    public class DatabaseContext : IdentityDbContext<User>
    {
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DatabaseContext() : base() {}
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options){}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().Ignore(f => f.PhoneNumber);
            builder.Entity<User>().Ignore(f => f.PhoneNumberConfirmed);
            builder.Entity<User>().Ignore(f => f.TwoFactorEnabled);
            builder.Entity<User>().Ignore(f => f.LockoutEnabled);
            builder.Entity<User>().Ignore(f => f.LockoutEnd);
            builder.Entity<User>().Ignore(f => f.AccessFailedCount);
            builder.Entity<User>().Ignore(f => f.EmailConfirmed);
            builder.Entity<User>().Ignore(f => f.PasswordHash);
        }
    }
}
