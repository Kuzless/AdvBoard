using AdvBoard.Domain.Interfaces;
using AdvBoard.Infrastructure.Repositories;

namespace AdvBoard.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    { 
        private readonly DatabaseContext _context;
        private bool IsDisposed;

        public IAnnouncementRepository AnnouncementRepository { get; private set; }
        public IUserRepository UserRepository { get; private set; }
        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
            AnnouncementRepository = new AnnouncementRepository(_context);
            UserRepository = new UserRepository(_context);
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                IsDisposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
