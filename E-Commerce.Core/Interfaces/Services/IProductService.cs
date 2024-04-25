using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Specifications;

namespace E_Commerce.Core.Interfaces.Services
{
    public interface IProductService
    {
        Task<PagenatedResultDto<ProductToReturnDto>> GetAllProductsAsync(ProductSpecificationsParams specParam);
        Task<ProductToReturnDto> GetProductsAsync(int id);
        Task<IEnumerable<BrandTypeDto>> GetAllBrandsAsync();
        Task<IEnumerable<BrandTypeDto>> GetAllTypesAsync();
    }
}
