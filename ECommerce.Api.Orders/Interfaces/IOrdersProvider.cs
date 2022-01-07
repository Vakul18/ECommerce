
namespace ECommerce.Api.Orders.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ECommerce.Api.Orders.Models;

    public interface IOrdersProvider
    {
        public Task<(bool IsSuccess, IEnumerable<Order> Orders,string ErrorMessage)> GetCustomerOrdersAsync(int customerId);

        public Task<(bool IsSuccess, Order @Order,string ErrorMessage)> GetOrderAsync(int id);
    }
}