using E_Commerce.Core.Entities;
using E_Commerce.Core.Specifications;

namespace E_Commerce.Repository.Specifications
{
    public class ProductCountWithSpec : BaseSpecifications<Product>
    {
        public ProductCountWithSpec(ProductSpecificationsParams specParams)
            : base(product =>
            (!specParams.TypeId.HasValue || product.TypeId == specParams.TypeId.Value)
        && (!specParams.BrandId.HasValue || product.BrandId == specParams.BrandId.Value) &&
            (string.IsNullOrWhiteSpace(specParams.Search) || product.Name.ToLower().Contains(specParams.Search)))
        {
               
        }
    }
}
