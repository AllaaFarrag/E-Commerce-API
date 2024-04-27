using AutoMapper;
using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Entities.Order;
using E_Commerce.Core.Interfaces;
using E_Commerce.Core.Interfaces.Repositories;
using E_Commerce.Core.Interfaces.Services;
using E_Commerce.Repository.Specifications;
using Microsoft.Extensions.Configuration;
using Stripe;
using Product = E_Commerce.Core.Entities.Product;

namespace E_Commerce.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketService _basketService;
        private readonly IConfiguration _configuration;

        public PaymentService(IMapper mapper, IUnitOfWork unitOfWork, IBasketService basketService, IConfiguration configuration)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _basketService = basketService;
            _configuration = configuration;
        }


        public async Task<BasketDto> CreateOrUpdatePaymentIntentForExistingOrder(BasketDto basket)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            //1. Calculate Amount
            //1.1 Total => Price * Quantity
            foreach (var item in basket.BasketItems)
            {
                //Check Product Price
                var product = await _unitOfWork.Repository<Product, int>().GetAsync(item.ProductId);
                if (product?.Price != item.Price) item.Price = product.Price;

            }
            var total = basket.BasketItems.Sum(item => item.Price * item.Quantity);

            //1.2 Shipping Price
            if (!basket.DeliveryMethodId.HasValue) throw new Exception("No Delivery Method Selected");

            var deliveyMethod = await _unitOfWork.Repository<DeliveryMethods, int>().GetAsync(basket.DeliveryMethodId.Value);
            var shippingPrice = deliveyMethod.Price;
            basket.ShippingPrice = deliveyMethod.Price;

            //Calcualte Amount in thr Smallest unit
            long amount = (long)((total*100) + (shippingPrice*100));

            //2. Create or Update
            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if (!string.IsNullOrWhiteSpace(basket.PaymentIntentId)) 
            {
                //Create
                var options = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency ="USD",
                    PaymentMethodTypes = new List<string> {"card"},
                };
                paymentIntent = await service.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;

            }
            else
            {
                //Update
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = amount,
                };
                await service.UpdateAsync(basket.PaymentIntentId,options);

            }
            await _basketService.UpdateBasketAysnc(basket);
            return basket;
        }

        public async Task<BasketDto> CreateOrUpdatePaymentIntentForNewOrder(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            //0. Get Basket by Id
            var basket = await _basketService.GetBasketAysnc(basketId);

            //1. Calculate Amount
            //1.1 Total => Price * Quantity
            foreach (var item in basket.BasketItems)
            {
                //Check Product Price
                var product = await _unitOfWork.Repository<Product, int>().GetAsync(item.ProductId);
                if (product?.Price != item.Price) item.Price = product.Price;

            }
            var total = basket.BasketItems.Sum(item => item.Price * item.Quantity);

            //1.2 Shipping Price
            if (!basket.DeliveryMethodId.HasValue) throw new Exception("No Delivery Method Selected");

            var deliveyMethod = await _unitOfWork.Repository<DeliveryMethods, int>().GetAsync(basket.DeliveryMethodId.Value);
            var shippingPrice = deliveyMethod.Price;
            basket.ShippingPrice = deliveyMethod.Price;


            //Calcualte Amount in thr Smallest unit
            long amount = (long)((total * 100) + (shippingPrice * 100));

            //2. Create or Update
            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if (!string.IsNullOrWhiteSpace(basket.PaymentIntentId))
            {
                //Create
                var options = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "card" },
                };
                paymentIntent = await service.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;

            }
            else
            {
                //Update
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = amount,
                };
                await service.UpdateAsync(basket.PaymentIntentId, options);

            }
            await _basketService.UpdateBasketAysnc(basket);
            return basket;
        }

        public async Task<OrderResultDto> UpdatePaymentStatusFailed(string paymentIntentId)
        {
            //1. Get Order by paymentIntentId => new Spec Class
            var spec = new OrderWithPaymentIntentIdSpecifications(paymentIntentId);
            var order = await _unitOfWork.Repository<Order , Guid>().GetWithSpecAsync(spec);

            if (order is null) throw new Exception($"No Order with PaymentIntendId {paymentIntentId}");

            //2. Update Payment Status
            order.PaymentStatus = PaymentStatus.Failed;
            _unitOfWork.Repository<Order,Guid>().Update(order);
            //3. Save Changes
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<OrderResultDto>(order);
        }

        public async Task<OrderResultDto> UpdatePaymentStatusSucceded(string paymentIntentId)
        {
            //1. Get Order by paymentIntentId => new Spec Class
            var spec = new OrderWithPaymentIntentIdSpecifications(paymentIntentId);
            var order = await _unitOfWork.Repository<Order, Guid>().GetWithSpecAsync(spec);

            if (order is null) throw new Exception($"No Order with PaymentIntendId {paymentIntentId}");

            //2. Update Payment Status
            order.PaymentStatus = PaymentStatus.Received;
            _unitOfWork.Repository<Order, Guid>().Update(order);
            //3. Save Changes
            await _unitOfWork.CompleteAsync();

            //4. Delete Customer Basket Content
            await _basketService.DeleteBasketAysnc(order.BasketId);

            return _mapper.Map<OrderResultDto>(order);
        }
    }
}
