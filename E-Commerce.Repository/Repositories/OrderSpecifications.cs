using E_Commerce.Core.Entities.Order;
using E_Commerce.Repository.Specifications;
using System.Linq.Expressions;

namespace E_Commerce.Repository.Repositories
{
    public class OrderSpecifications : BaseSpecifications<Order>
    {
        public OrderSpecifications(string email) 
            : base(order => order.BuyerEmail == email)
        {
            IncludeExpressions.Add(order => order.DeliveryMethod);
            IncludeExpressions.Add(order => order.OrderItems);

            OrderByDesc = o => o.OrderDate;

        }

        public OrderSpecifications(Guid id , string email)
            : base(order => order.BuyerEmail == email && order.Id == id)
        {
            IncludeExpressions.Add(order => order.DeliveryMethod);
            IncludeExpressions.Add(order => order.OrderItems);
        }
    }
}
