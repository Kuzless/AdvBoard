using AdvBoard.Domain.Entities;
using AdvBoard.Domain.Interfaces;
using AdvBoard.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;

namespace AdvBoard.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    { 
        private readonly DatabaseContext _context;
        private bool IsDisposed;

        public IAnnouncementRepository AnnouncementRepository { get; private set; }
        public ISubCategoryRepository SubCategoryRepository { get; private set; }
        public ICategoryRepository CategoryRepository { get; private set; }
        public IStatusRepository StatusRepository { get; private set; }
        public UserManager<User> UserManager { get; private set; }
        public UnitOfWork(DatabaseContext context, UserManager<User> userManager)
        {
            _context = context;
            AnnouncementRepository = new AnnouncementRepository(_context);
            SubCategoryRepository = new SubCategoryRepository(_context);
            CategoryRepository = new CategoryRepository(_context);
            StatusRepository = new StatusRepository(_context);
            UserManager = userManager;
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
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
