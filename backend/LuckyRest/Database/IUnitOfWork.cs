using LuckyRest.Database.DAOs.WorkshopMapDao;
using LuckyRest.Database.DAOs.WorkshopPlaylistDao;
using LuckyRest.Database.DAOs.WorkshopPlaylistMapDao;

namespace LuckyRest.Database;

public interface IUnitOfWork
{
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}