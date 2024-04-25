using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Entities.Order;

namespace E_Commerce.Core.Interfaces.Services
{
    public interface IOrderService
    {
        public Task<IEnumerable<DeliveryMethods>> GetDeliveryMethodsAsync();

        public Task<OrderResultDto> CreateOrderAsync(OrderDto input);
        public Task<OrderResultDto> GetOrderAsync(Guid id, string email);
        public Task<IEnumerable<OrderResultDto>> GetAllOrderAsync(string email);
        
    }
}
