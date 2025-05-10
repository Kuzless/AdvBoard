using AdvBoard.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace AdvBoard.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveAsync();
        IAnnouncementRepository AnnouncementRepository { get; }
        IUserRepository UserRepository { get; }
        UserManager<User> UserManager { get; }
    }
}
