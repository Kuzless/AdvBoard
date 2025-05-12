using AdvBoard.Domain.Entities;
using AdvBoard.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdvBoard.Infrastructure.Repositories
{
    public class AnnouncementRepository : GenericRepository<Announcement>, IAnnouncementRepository
    {
        
        public AnnouncementRepository(DatabaseContext context) : base(context){}

        public async Task<Announcement> GetByIdAsync(int id)
        {
            return await _context.Set<Announcement>()
                .Include(a => a.SubCategory)
                .ThenInclude(s => s.Category)
                .Include(a => a.User)
                .Include(a => a.Status)
                .FirstAsync(a => a.Id == id);
        }
        public async Task<List<Announcement>> GetAnnouncementsPageAsync()
        {
            return await _context.Set<Announcement>()
                .Include(a => a.SubCategory)
                .ThenInclude(s => s.Category)
                .Include(a => a.User)
                .Include(a => a.Status).ToListAsync();
        }

        public async Task<List<Announcement>> GetAnnouncementsByUserIdAsync(string userId)
        {
            return await _context.Set<Announcement>()
                .Where(a => a.UserId == userId)
                .Include(a => a.SubCategory)
                .ThenInclude(s => s.Category)
                .Include(a => a.Status).ToListAsync();
        }
    }
}
