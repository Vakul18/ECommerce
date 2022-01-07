using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using Microsoft.Extensions.Logging;

namespace ECommerce.Api.Search.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly ILogger<CustomersService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public CustomersService(ILogger<CustomersService> logger, IHttpClientFactory httpClientFactory)
        {
            this._logger = logger;
            this._httpClientFactory = httpClientFactory;
        }

        async Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> ICustomersService.GetCustomerAsync(int customerId)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient(typeof(CustomersService).Name);
                var response = await httpClient.GetAsync($"api/v1/customers/{customerId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var result = JsonSerializer.Deserialize<Customer>(content, new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return (true, result, null);
                }
                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}