using E_Commerce.Core.Entities.Order;

namespace E_Commerce.Core.DataTransferObjects
{
    public class OrderResultDto
    {
        public Guid Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; } 
        public AddressDto ShippingAddress { get; set; }

        public string DeliveryMethod { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ShippingPrice { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? BasketId { get; set; }

        public decimal Total { get; set; }

    }
}
