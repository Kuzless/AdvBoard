using AdvBoard.Domain.Entities;

namespace AdvBoard.Domain.Interfaces
{
    public interface IAnnouncementRepository : IGenericRepository<Announcement>
    {
        Task<Announcement> GetByIdAsync(int id);
    }
}
