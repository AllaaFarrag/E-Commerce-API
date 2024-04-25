using E_Commerce.Core.Entities;
using E_Commerce.Core.Specifications;

namespace E_Commerce.Repository.Specifications
{
    public class ProductSpecifications : BaseSpecifications<Product>
    {

        // Get Products with Filteration  {brandId , TypeId}
        public ProductSpecifications(ProductSpecificationsParams specs) : 
            base(product => 
            (!specs.TypeId.HasValue || product.TypeId == specs.TypeId.Value)
        && (!specs.BrandId.HasValue || product.BrandId == specs.BrandId.Value)&&
            (string.IsNullOrWhiteSpace(specs.Search) || product.Name.ToLower().Contains(specs.Search)))
        {
            IncludeExpressions.Add(product => product.ProductBrand);
            IncludeExpressions.Add(product => product.ProductType);

            ApplyPagenation(specs.PageSize , specs.PageIndex);

            if (specs.Sort is not null)
            {
                switch (specs.Sort)
                {
                    case ProductSortingParams.NameAsc:
                        OrderBy = x => x.Name; 
                        break;

                    case ProductSortingParams.NameDesc:
                        OrderByDesc = x => x.Name;
                        break;

                    case ProductSortingParams.PriceAsc:
                        OrderBy = x => x.Price;
                        break;

                    case ProductSortingParams.PriceDesc:
                        OrderByDesc = x => x.Price;
                        break;

                    default:
                        OrderBy = x => x.Name;
                        break;
                }
            }
            else OrderBy = x => x.Name;
        }

        //Get product by id
        public ProductSpecifications(int id) 
            : base(product => product.Id == id)
        {
            IncludeExpressions.Add(product => product.ProductBrand);
            IncludeExpressions.Add(product => product.ProductType);
        } 
    }
}
