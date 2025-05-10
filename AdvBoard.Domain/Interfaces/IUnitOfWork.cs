namespace AdvBoard.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveAsync();
    }
}
