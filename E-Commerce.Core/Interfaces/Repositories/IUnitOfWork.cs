using E_Commerce.Core.Entities;

namespace E_Commerce.Core.Interfaces.Repositories
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        //Signture for method
        IGenericRepository<TEntity , TKey> Repository<TEntity , TKey>() where TEntity : BaseEntity<TKey>;
        Task<int> CompleteAsync();
    }
}
