using AdvBoard.Domain.Entities;
using AdvBoard.Domain.Interfaces;

namespace AdvBoard.Infrastructure.Repositories
{
    public class StatusRepository : GenericRepository<Status>, IStatusRepository
    {
        public StatusRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
