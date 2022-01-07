
namespace ECommerce.Api.Orders.Controllers
{
    using System.Threading.Tasks;
    using ECommerce.Api.Orders.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/v1/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProvider _ordersProvider;

        public OrdersController(IOrdersProvider ordersProvider)
        {
            this._ordersProvider = ordersProvider;
        }

        [HttpGet()]
        [Route("customer/{customerId}")]
        async public Task<IActionResult> GetCustomerOrders(int customerId)
        {
            var result = await _ordersProvider.GetCustomerOrdersAsync(customerId);
            if(result.IsSuccess)
            {
                return Ok(result.Orders);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        async public Task<IActionResult> GetOrders(int id)
        {
            var result = await _ordersProvider.GetOrderAsync(id);
            if(result.IsSuccess)
            {
                return Ok(result.Order);
            }
            return NotFound();
        }

    }
}