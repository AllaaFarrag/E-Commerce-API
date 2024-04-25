using AutoMapper;
using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Entities.Order;
using E_Commerce.Core.Interfaces.Repositories;
using E_Commerce.Core.Interfaces.Services;
using E_Commerce.Repository.Repositories;

namespace E_Commerce.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketService _basketService;

        public OrderService(IMapper mapper, IUnitOfWork unitOfWork, IBasketService basketService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _basketService = basketService;
        }

        public async Task<OrderResultDto> CreateOrderAsync(OrderDto input)
        {
            // 1. Get Basket
            var basket = await _basketService.GetBasketAysnc(input.BasketId);
            if (basket == null) throw new Exception($"No Basket with id {input.BasketId} found");
            
            // 2. Create OrderItems List and Get OrderItems from Basket Items
            var orderItems = new List<OrderItem>();
            foreach (var BasketItem in basket.BasketItems)
            {
                var product = await _unitOfWork.Repository<Product, int>().GetAsync(BasketItem.ProductId);
                if (product == null) continue;

                var productItem = new OrderItemProduct
                {
                    PictureUrl= product.PictureUrl,
                    ProductId = product.Id,
                    ProductName = product.Name,
                };

                var orderItem = new OrderItem
                {
                    OrderItemProduct = productItem,
                    Price = product.Price,
                    Quantity = BasketItem.Quantity
                };

                //var mappedItem = _mapper.Map<OrderItemDto>(orderItem);
                orderItems.Add(orderItem);
            }

            if (!orderItems.Any()) throw new Exception("No Basket Items Found");

            // 3. Delivery Method
            if(!input.DeliveryMethodId.HasValue) throw new Exception("No Delivery Method Selected");

            var delivery = await _unitOfWork.Repository<DeliveryMethods , int>().GetAsync(input.DeliveryMethodId.Value);

            if (delivery == null) throw new Exception("Invalid Delivery Method Id");

            // 4. Shipping Address

            var shippingAddress = _mapper.Map<ShippingAddress>(input.ShippingAddress);

            // 5. Calculate Sub Total  {Price * Quantity}

            var subTotal = orderItems.Sum(items => items.Price * items.Quantity);

            // 6. Map From OrderItemDto => OrderItem

            var mappedItems = _mapper.Map<List<OrderItem>>(orderItems);

            var order = new Order
            {
                BuyerEmail = input.BuyerEmail,
                ShippingAddress = shippingAddress,
                DeliveryMethod = delivery,
                OrderItems = mappedItems,
                SubTotal = subTotal
            };

            await _unitOfWork.Repository<Order, Guid>().AddAysnc(order);

            return _mapper.Map<OrderResultDto>(order);
        }

        public async Task<IEnumerable<OrderResultDto>> GetAllOrderAsync(string email)
        {
            var specs = new OrderSpecifications(email);

            var orders = await _unitOfWork.Repository<Order, Guid>().GetAllWithSpecAsync(specs);

            return _mapper.Map<IEnumerable<OrderResultDto>>(orders);
        }

        public async Task<OrderResultDto> GetOrderAsync(Guid id, string email)
        {
            var specs = new OrderSpecifications(id , email);

            var orders = await _unitOfWork.Repository<Order, Guid>().GetWithSpecAsync(specs);

            return _mapper.Map<OrderResultDto>(orders);
        }

        public async Task<IEnumerable<DeliveryMethods>> GetDeliveryMethodsAsync()  
            => await _unitOfWork.Repository<DeliveryMethods , int>().GetAllAsync();

    }
}
