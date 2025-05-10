using AdvBoard.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace AdvBoard.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveAsync();
        IAnnouncementRepository AnnouncementRepository { get; }
        ISubCategoryRepository SubCategoryRepository { get; }
        UserManager<User> UserManager { get; }
    }
}
