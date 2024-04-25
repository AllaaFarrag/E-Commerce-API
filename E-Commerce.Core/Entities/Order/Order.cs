using System.Text.Json.Serialization;

namespace E_Commerce.Core.Entities.Order
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PaymentStatus { Pending , Failed , Received}


    public class Order : BaseEntity<Guid>
    {
        public string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public ShippingAddress ShippingAddress { get; set; }

        public DeliveryMethods DeliveryMethod { get; set; }
        public int? DeliveryMethodId { get; set; }

        public IEnumerable<OrderItem> OrderItems { get; set; }
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
        public decimal SubTotal { get; set; }

        public decimal Total() => SubTotal + DeliveryMethod.Price;

    }
}
