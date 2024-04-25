using E_Commerce.Core.Entities;
using E_Commerce.Core.Interfaces.Repositories;
using E_Commerce.Core.Interfaces.Specifications;
using E_Commerce.Repository.Context;
using E_Commerce.Repository.Specifications;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository.Repositories
{
    public class GenericRepository<TEntity, Tkey> : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        private readonly DataContext _context;

        public GenericRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddAysnc(TEntity entity) => await _context.Set<TEntity>().AddAsync(entity); 

        public void Delete(TEntity entity) =>  _context.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity) =>  _context.Set<TEntity>().Update(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _context.Set<TEntity>().ToListAsync();

        public async Task<TEntity> GetAsync(Tkey id) => (await _context.Set<TEntity>().FindAsync(id))!;

        public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity> specifications)
            => await ApplySpecifications(specifications).ToListAsync();
        

        public async Task<TEntity> GetWithSpecAsync(ISpecifications<TEntity> specifications)
            => await ApplySpecifications(specifications).FirstOrDefaultAsync();


        private IQueryable<TEntity> ApplySpecifications(ISpecifications<TEntity> specifications) => SpecificationsEvaluator<TEntity, Tkey>.BuildQuery(_context.Set<TEntity>(), specifications);

        public async Task<int> GetProductCountWithSpec(ISpecifications<TEntity> specifications)
        => await ApplySpecifications(specifications).CountAsync();
    }
}
