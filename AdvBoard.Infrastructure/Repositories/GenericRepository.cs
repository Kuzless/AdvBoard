using AdvBoard.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace AdvBoard.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T: class
    {
        protected readonly DatabaseContext _context;
        public GenericRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
    }
}
