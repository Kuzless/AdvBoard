using AdvBoard.Domain.Interfaces;

namespace AdvBoard.Infrastructure.Repositories
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly DatabaseContext _context;
        public AnnouncementRepository(DatabaseContext context)
        {
            _context = context;
        }
    }
}
