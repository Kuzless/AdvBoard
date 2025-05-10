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

            // seed
            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Побутова техніка" },
                new Category { Id = 2, Name = "Комп'ютерна техніка" },
                new Category { Id = 3, Name = "Смартфони" },
                new Category { Id = 4, Name = "Інше" }
            );
            builder.Entity<SubCategory>().HasData(
                new SubCategory { Id = 1, Name = "Холодильники", CategoryId = 1 },
                new SubCategory { Id = 2, Name = "Пральні машини", CategoryId = 1 },
                new SubCategory { Id = 3, Name = "Бойлери", CategoryId = 1 },
                new SubCategory { Id = 4, Name = "Печі", CategoryId = 1 },
                new SubCategory { Id = 5, Name = "Витяжки", CategoryId = 1 },
                new SubCategory { Id = 6, Name = "Мікрохвильові печі", CategoryId = 1 },
                new SubCategory { Id = 7, Name = "ПК", CategoryId = 2 },
                new SubCategory { Id = 8, Name = "Ноутбуки", CategoryId = 2 },
                new SubCategory { Id = 9, Name = "Монітори", CategoryId = 2 },
                new SubCategory { Id = 10, Name = "Принтери", CategoryId = 2 },
                new SubCategory { Id = 11, Name = "Сканери", CategoryId = 2 },
                new SubCategory { Id = 12, Name = "Android смартфони", CategoryId = 3 },
                new SubCategory { Id = 13, Name = "iOS/Apple смартфони", CategoryId = 3 },
                new SubCategory { Id = 14, Name = "Одяг", CategoryId = 4 },
                new SubCategory { Id = 15, Name = "Взуття", CategoryId = 4 },
                new SubCategory { Id = 16, Name = "Аксесуари", CategoryId = 4 },
                new SubCategory { Id = 17, Name = "Спортивне обладнання", CategoryId = 4 },
                new SubCategory { Id = 18, Name = "Іграшки", CategoryId = 4 }
            );
            builder.Entity<Status>().HasData(
                new Status { Id = 1, Name = "Активне" },
                new Status { Id = 2, Name = "Неактивне" }
            );
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (var entity in ChangeTracker.Entries<Announcement>())
            {
                entity.Property("CreatedAt").IsModified = false;
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
