using AutoMapper;
using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Interfaces.Repositories;
using E_Commerce.Core.Interfaces.Services;
using E_Commerce.Core.Specifications;
using E_Commerce.Repository.Specifications;

namespace E_Commerce.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BrandTypeDto>> GetAllBrandsAsync()
        {
            var brands =await _unitOfWork.Repository<ProductBrand , int>().GetAllAsync();
            return _mapper.Map <IEnumerable<BrandTypeDto>>(brands);   
        }

        public async Task<PagenatedResultDto<ProductToReturnDto>> GetAllProductsAsync(ProductSpecificationsParams specParams )
        {
            var spec = new ProductSpecifications(specParams);
            var products = await _unitOfWork.Repository<Product, int>().GetAllWithSpecAsync(spec);
            var countSpec = new ProductCountWithSpec(specParams);
            var count = await _unitOfWork.Repository<Product, int>().GetProductCountWithSpec(countSpec);
            var mappedProducts = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);
            return new PagenatedResultDto<ProductToReturnDto> 
            { 
                Data = mappedProducts,
                PageIndex = specParams.PageIndex,
                PageSize = specParams.PageSize,
                TotalCount = count,
            };
        }

        public async Task<IEnumerable<BrandTypeDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<BrandTypeDto>>(types);
        }

        public async Task<ProductToReturnDto> GetProductsAsync(int id)
        {
            var spec = new ProductSpecifications(id);
            var product = await _unitOfWork.Repository<Product, int>().GetWithSpecAsync(spec);
            return _mapper.Map<ProductToReturnDto>(product);
        }
    }
} 
