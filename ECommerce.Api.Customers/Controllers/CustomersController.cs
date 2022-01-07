
namespace ECommerce.Api.Customers.Controllers
{
    using System.Threading.Tasks;
    using ECommerce.Api.Customers.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/v1/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersProvider _customersProvider;

        public CustomersController(ICustomersProvider customersProvider)
        {
            this._customersProvider = customersProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var result = await _customersProvider.GetCustomers();
            if(result.IsSuccess)
            {
                return Ok(result.Customers);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomers(int id)
        {
            var result = await _customersProvider.GetCustomer(id);
            if(result.IsSuccess)
            {
                return Ok(result.Customer);
            }
            return NotFound();
        }

    }
}