using E_Commerce.Core.Entities;
using E_Commerce.Core.Interfaces.Specifications;

namespace E_Commerce.Core.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity> specifications);
        Task<int> GetProductCountWithSpec(ISpecifications<TEntity> specifications);
        Task<TEntity> GetAsync(TKey id);
        Task<TEntity> GetWithSpecAsync(ISpecifications<TEntity> specifications);
        Task AddAysnc(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);

    }
}
