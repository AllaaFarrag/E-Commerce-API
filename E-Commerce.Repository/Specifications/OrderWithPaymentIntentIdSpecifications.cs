using E_Commerce.Core.Entities.Order;

namespace E_Commerce.Repository.Specifications
{
    public class OrderWithPaymentIntentIdSpecifications : BaseSpecifications<Order>
    {
        public OrderWithPaymentIntentIdSpecifications(string paymentIntentId)
            : base(order => order.PaymentIntentId == paymentIntentId)
        {
            IncludeExpressions.Add(o => o.DeliveryMethod);
        }
    }
}
