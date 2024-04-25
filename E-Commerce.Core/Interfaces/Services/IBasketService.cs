using E_Commerce.Core.DataTransferObjects;

namespace E_Commerce.Core.Interfaces.Services
{
    public interface IBasketService
    {
        Task<BasketDto?> GetBasketAysnc(string id);
        Task<BasketDto?> UpdateBasketAysnc (BasketDto basket);
        Task<bool> DeleteBasketAysnc (string id);
    }
}
