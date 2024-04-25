using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Interfaces.Specifications
{
    public interface ISpecifications<T>
    {
        //Where Criteria
        public Expression<Func<T,bool>> Criteria { get; }

        //Include 
        public List<Expression<Func<T ,object>>> IncludeExpressions { get;}

        //OrderBy
        public Expression<Func<T , object>> OrderBy { get;}
        public Expression<Func<T , object>> OrderByDesc { get;}

        //Pagenation {Skip , Take}
        public int Skip { get;}
        public int Take { get;}
        public bool IsPagenated { get;}
    }
}
