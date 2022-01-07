
namespace ECommerce.Api.Search.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ECommerce.Api.Search.Models;

    public interface IOrdersService
    {
        public Task<(bool IsSuccess,IEnumerable<Order> Orders,string ErrorMessage)> GetOrdersAsync(int customerId);
    }
}