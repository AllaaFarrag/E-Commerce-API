using AutoMapper;
using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Entities.Basket;
using E_Commerce.Core.Interfaces.Repositories;
using E_Commerce.Core.Interfaces.Services;

namespace E_Commerce.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _repository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteBasketAysnc(string id) => await _repository.DeleteCustomerBasketAsync(id);

        public async Task<BasketDto?> GetBasketAysnc(string id)
        {
            var basket = await _repository.GetCustomerBasketAsync(id);
            return basket is null ? null : _mapper.Map<BasketDto?>(basket);
        }

        public async Task<BasketDto?> UpdateBasketAysnc(BasketDto basket)
        {
            var customerbasket = _mapper.Map<CustomerBasket>(basket);
            var updatebasket = await _repository.UpdateCustomerBasketAsync(customerbasket);
            return updatebasket is null ? null : _mapper.Map<BasketDto>(updatebasket);
        }
    }
}
