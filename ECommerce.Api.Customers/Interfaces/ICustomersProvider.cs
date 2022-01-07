
namespace ECommerce.Api.Customers.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ECommerce.Api.Customers.Models;
    public interface ICustomersProvider
    {
        public Task<(bool IsSuccess, IEnumerable<Customer> Customers, string ErrorMessage)> GetCustomers();
        public Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> GetCustomer(int id);
    }
}