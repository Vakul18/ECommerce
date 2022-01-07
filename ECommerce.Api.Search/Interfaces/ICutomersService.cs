
namespace ECommerce.Api.Search.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ECommerce.Api.Search.Models;

    public interface ICustomersService
    {
        public Task<(bool IsSuccess,Customer Customer,string ErrorMessage)> GetCustomerAsync(int customerId);
    }
}