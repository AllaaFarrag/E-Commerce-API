using E_Commerce.Core.Entities.Basket;

namespace E_Commerce.Core.DataTransferObjects
{
    public class BasketDto
    {
        public string Id { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }
        public List<BasketItemDto> BasketItems { get; set; } = new();
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
    }
}
