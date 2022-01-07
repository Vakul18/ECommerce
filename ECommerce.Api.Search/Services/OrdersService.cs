
namespace ECommerce.Api.Search.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using ECommerce.Api.Search.Interfaces;
    using ECommerce.Api.Search.Models;
    using Microsoft.Extensions.Logging;
    using System.Text.Json;

    public class OrdersService : IOrdersService
    {
        private readonly ILogger<OrdersService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public OrdersService(ILogger<OrdersService> logger, IHttpClientFactory httpClientFactory)
        {
            this._logger = logger;
            this._httpClientFactory = httpClientFactory;
        }

        async Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> IOrdersService.GetOrdersAsync(int customerId)
        {
            try
            {
                 var httpClient = _httpClientFactory.CreateClient(typeof(OrdersService).Name);
                 var response = await httpClient.GetAsync($"api/v1/orders/customer/{customerId}");
                 if(response.IsSuccessStatusCode)
                 {
                     var content = await response.Content.ReadAsByteArrayAsync();
                     var result = JsonSerializer.Deserialize<IEnumerable<Order>>(content, new JsonSerializerOptions()
                     {
                         PropertyNameCaseInsensitive = true
                     });
                     return (true,result,null);
                 }
                 return (false,null,response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false,null,ex.Message);
            }
        }
    }
}