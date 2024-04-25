using E_Commerce.Core.Interfaces.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Specifications
{
    public class BaseSpecifications<T> : ISpecifications<T>
    {
        public Expression<Func<T, bool>> Criteria { get; }
        public BaseSpecifications(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public List<Expression<Func<T, object>>> IncludeExpressions { get; } = new();

        public Expression<Func<T, object>> OrderBy {get; protected set;}

        public Expression<Func<T, object>> OrderByDesc { get; protected set; }

        public int Skip { get; protected set; }

        public int Take { get; protected set; }

        public bool IsPagenated { get; protected set; }

        protected void ApplyPagenation(int PageSize , int PageIndex)
        {
            IsPagenated = true;
            Take = PageSize;
            Skip = (PageIndex - 1 ) * PageSize;
        }
    }
}
