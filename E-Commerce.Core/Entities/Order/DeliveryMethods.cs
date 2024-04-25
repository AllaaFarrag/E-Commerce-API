﻿namespace E_Commerce.Core.Entities.Order
{
    public class DeliveryMethods : BaseEntity<int>
    {
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string DeliveryTime { get; set; }
        public decimal Price { get; set; }

    }
}
