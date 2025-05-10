using AdvBoard.Domain.Entities;
using AdvBoard.Domain.Interfaces;

namespace AdvBoard.Infrastructure.Repositories
{
    public class AnnouncementRepository : GenericRepository<Announcement>, IAnnouncementRepository
    {
        
        public AnnouncementRepository(DatabaseContext context) : base(context){}

        public async Task<Announcement> GetByIdAsync(int id)
        {
            var adv = await _context.Set<Announcement>().FindAsync(id);
            return adv!;
        }
    }
}
