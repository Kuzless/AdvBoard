using AdvBoard.Domain.Entities;

namespace AdvBoard.Domain.Interfaces
{
    public interface ISubCategoryRepository : IGenericRepository<SubCategory>
    {
        Task<SubCategory> GetSubCategoryByIdAsync(int id);
    }
}
