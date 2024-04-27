using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Entities.Order;
using E_Commerce.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<OrderResultDto>> Create(OrderDto input)
        {
            var order = await _orderService.CreateOrderAsync(input);
            return Ok(order);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResultDto>>> GetOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order = await _orderService.GetAllOrderAsync(email);

            return Ok(order);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResultDto>> GetOrders(Guid id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order = await _orderService.GetOrderAsync(id, email);

            return Ok(order);
        }

        [HttpGet("Delivery")]
        public async Task<ActionResult<DeliveryMethods>> GetDelivaryMethod() 
            => Ok(await _orderService.GetDeliveryMethodsAsync());
    }
}
