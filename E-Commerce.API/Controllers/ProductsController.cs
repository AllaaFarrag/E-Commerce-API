using E_Commerce.API.Errors;
using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Interfaces.Services;
using E_Commerce.Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecificationsParams specParams)
            => Ok(await _productService.GetAllProductsAsync(specParams));

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
           // throw new NotImplementedException();
            var product = await _productService.GetProductsAsync(id);
            return product is not null ? Ok(product) : NotFound(new ApiResponse(404 , $"Product with id : {id} Not Found"));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<BrandTypeDto>> GetBrands()
            => Ok(await _productService.GetAllBrandsAsync());

        [HttpGet("Types")]
        public async Task<ActionResult<BrandTypeDto>> GetTypes()
            => Ok(await _productService.GetAllTypesAsync());

    }
}
