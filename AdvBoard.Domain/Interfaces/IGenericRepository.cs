using AdvBoard.Domain.Entities;

namespace AdvBoard.Domain.Interfaces
{
    public interface IGenericRepository<T> where T: class
    {
        Task AddAsync(T entity);
        Task Update(T entity);
    }
}
