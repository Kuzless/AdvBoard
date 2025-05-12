using AdvBoard.Domain.Entities;
using AdvBoard.Domain.Interfaces;

namespace AdvBoard.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
