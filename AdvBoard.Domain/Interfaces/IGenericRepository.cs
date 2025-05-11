namespace AdvBoard.Domain.Interfaces
{
    public interface IGenericRepository<T> where T: class
    {
        Task AddAsync(T entity);
        Task Delete(T entity);
    }
}
