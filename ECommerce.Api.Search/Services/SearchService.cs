using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService _ordersService;
        private readonly IProductsService _productsService;
        private readonly ICustomersService _customersService;

        public SearchService(IOrdersService ordersService, IProductsService productsService, ICustomersService customersService)
        {
            this._ordersService = ordersService;
            this._productsService = productsService;
            this._customersService = customersService;
        }

        async Task<(bool IsSuccess, dynamic SearchResults)> ISearchService.SearchAsync(int customerId)
        {
            var ordersResult = await _ordersService.GetOrdersAsync(customerId);
            var customersResult = await _customersService.GetCustomerAsync(customerId);
            var productsResult = await _productsService.GetProductsAsync();
            if (ordersResult.IsSuccess)
            {
                foreach (Order order in ordersResult.Orders)
                {
                    foreach (OrderItem item in order.Items)
                    {
                        item.ProductName = productsResult.IsSuccess ? productsResult.Products.FirstOrDefault(p => p.Id == item.Id)?.Name :
                         "Product Information Not available";
                    }
                }
                var result = new
                {
                    Order = ordersResult.Orders,
                    Customer = customersResult.IsSuccess ? customersResult.Customer :
                    new Customer() { Id = -1, Name = "Information Not available", Address = "Information Not available" }
                };
                return (true, result);
            }
            return (false, null);
        }



    }
}