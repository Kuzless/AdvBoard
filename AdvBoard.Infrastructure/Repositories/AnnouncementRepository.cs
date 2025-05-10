using AdvBoard.Domain.Entities;
using AdvBoard.Domain.Interfaces;

namespace AdvBoard.Infrastructure.Repositories
{
    public class AnnouncementRepository : GenericRepository<Announcement>, IAnnouncementRepository
    {
        
        public AnnouncementRepository(DatabaseContext context) : base(context){}
    }
}
