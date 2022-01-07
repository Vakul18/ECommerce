
namespace ECommerce.Api.Products.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ECommerce.Api.Products.Models;

    public interface IProductsProvider
    {
        Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMessge)> GetProductsAsync();
        Task<(bool IsSuccess, Product Product, string ErrorMessge)> GetProductsAsync(int id);
    }
}