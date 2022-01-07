using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Interfaces
{
    public interface IProductsService
    {
        public Task<(bool IsSuccess,IEnumerable<Product> Products,string ErrorMessage)> GetProductsAsync();
    }
}