using E_Commerce.Core.Entities.Basket;

namespace E_Commerce.Core.Interfaces.Repositories
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetCustomerBasketAsync(string id);
        Task<CustomerBasket?> UpdateCustomerBasketAsync(CustomerBasket basket);
        Task<bool> DeleteCustomerBasketAsync(string id);
    }
}
