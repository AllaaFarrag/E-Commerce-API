using E_Commerce.Core.DataTransferObjects;

namespace E_Commerce.Core.Interfaces
{
    public interface IPaymentService
    {
        public Task<BasketDto> CreateOrUpdatePaymentIntentForExistingOrder(BasketDto input);
        public Task<BasketDto> CreateOrUpdatePaymentIntentForNewOrder(string basketId);
        public Task<OrderResultDto> UpdatePaymentStatusFailed(string paymentIntentId);
        public Task<OrderResultDto> UpdatePaymentStatusSucceded(string paymentIntentId);
        
    }
}
