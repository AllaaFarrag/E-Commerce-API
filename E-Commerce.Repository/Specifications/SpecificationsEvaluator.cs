using E_Commerce.Core.Entities;
using E_Commerce.Core.Interfaces.Specifications;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository.Specifications
{
    public static class SpecificationsEvaluator<TEntity , TKey> where TEntity : BaseEntity<TKey>
    {
        public static IQueryable<TEntity> BuildQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity> specification)
        {
            var query = inputQuery;

            if (specification.Criteria is not null)
                query = query.Where(specification.Criteria);

            if (specification.OrderBy is not null)
                query = query.OrderBy(specification.OrderBy);

            if (specification.OrderByDesc is not null)
                query = query.OrderByDescending(specification.OrderByDesc);
             
            if (specification.IsPagenated)
                query = query.Skip(specification.Skip).Take(specification.Take);
            
            if (specification.IncludeExpressions.Any())
                query = specification.IncludeExpressions
                    .Aggregate(query, (currentquery, expression) => currentquery.Include(expression));


            //foreach (var item in specification.IncludeExpressions)
            //{
            //    query = query.Include(item);
            //}

            return query;
        }
    }
}
