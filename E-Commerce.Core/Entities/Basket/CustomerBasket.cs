﻿namespace E_Commerce.Core.Entities.Basket
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }
        public List<BasketItem> BasketItems { get; set; } = new();
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
    }
}
