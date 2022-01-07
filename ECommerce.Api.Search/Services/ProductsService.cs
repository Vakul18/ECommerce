using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using Microsoft.Extensions.Logging;

namespace   ECommerce.Api.Search.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ProductsService> _logger;

        public ProductsService(IHttpClientFactory httpClientFactory,ILogger<ProductsService> logger)
        {
            this._httpClientFactory = httpClientFactory;
            this._logger = logger;
        }

        async Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMessage)> IProductsService.GetProductsAsync()
        {
            try
            {
                 var httpClient = _httpClientFactory.CreateClient(typeof(ProductsService).Name);
                 var response = await httpClient.GetAsync("api/v1/products");
                 if(response.IsSuccessStatusCode)
                 {
                     var content = await response.Content.ReadAsByteArrayAsync();
                     var result = JsonSerializer.Deserialize<IEnumerable<Product>>(content, new JsonSerializerOptions()
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