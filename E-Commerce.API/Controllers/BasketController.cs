using E_Commerce.API.Errors;
using E_Commerce.API.Helper;
using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet("{id}")]
        [Cash(60)]
        public async Task<ActionResult<BasketDto>> Get(string id)
        {
            var basket = await _basketService.GetBasketAysnc(id);
            return basket is null ? NotFound(new ApiResponse(404 , $"Basket with id {id} Not Found")) : Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<BasketDto>> Update(BasketDto basketDto)
        {
            var basket = await _basketService.UpdateBasketAysnc(basketDto);
            return basket is null ? NotFound(new ApiResponse(404, $"Basket with id {basketDto.Id} Not Found")) : Ok(basket);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(string id) => Ok(await _basketService.DeleteBasketAysnc(id));
    }
}
