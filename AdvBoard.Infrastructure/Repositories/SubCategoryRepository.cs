using AdvBoard.Domain.Entities;
using AdvBoard.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdvBoard.Infrastructure.Repositories
{
    public class SubCategoryRepository : GenericRepository<SubCategory>, ISubCategoryRepository
    {
        public SubCategoryRepository(DatabaseContext context) : base(context){}

        public async Task<SubCategory> GetSubCategoryByIdAsync(int id)
        {
            var subCategory = await _context.Set<SubCategory>().Include(s => s.Category).FirstOrDefaultAsync(s => s.Id == id);
            return subCategory!;
        }
    }
}
