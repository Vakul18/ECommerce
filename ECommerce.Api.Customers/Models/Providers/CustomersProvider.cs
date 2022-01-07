
namespace ECommerce.Api.Customers.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ECommerce.Api.Customers.Db;
    using ECommerce.Api.Customers.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    internal class CustomersProvider : ICustomersProvider
    {
        private readonly CustomersDbContext _dbContext;
        private readonly ILogger<CustomersProvider> _logger;
        private readonly IMapper _mapper;

        public CustomersProvider(CustomersDbContext dbContext,ILogger<CustomersProvider> logger, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._logger = logger;
            this._mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (!_dbContext.Customers.Any())
            {
                _dbContext.Customers.AddRange(new List<Db.Customer>(){new Db.Customer(){Id = 1,Name="John",Address="C wing"}
            ,new Db.Customer(){Id = 2,Name="James",Address="A wing"}
            ,new Db.Customer(){Id = 5,Name="John",Address="B wing"}
            ,new Db.Customer(){Id = 3,Name="John",Address="D wing"}});
                _dbContext.SaveChanges();
            }
        }

        async Task<(bool IsSuccess, IEnumerable<Customer> Customers, string ErrorMessage)> ICustomersProvider.GetCustomers()
        {
            try
            {
                 var customers = await _dbContext.Customers.ToListAsync();
                 if(customers!=null && customers.Count>0)
                 {
                     var result = _mapper.Map<IEnumerable<Db.Customer>,IEnumerable<Models.Customer>>(customers);
                     return (true,result,null);
                 }
                 return (false,null,"Not Found");
            }
            catch (Exception ex)
            {                
                _logger.LogError(ex.ToString());            
                return (false,null,ex.Message);
            }
        }

        async Task<(bool IsSuccess, Customer @Customer, string ErrorMessage)> ICustomersProvider.GetCustomer(int id)
        {
            try
            {
                 var customer = await _dbContext.Customers.FindAsync(id);
                 if(customer!=null)
                 {
                     var result = _mapper.Map<Db.Customer,Models.Customer>(customer);
                     return (true,result,null);
                 }
                 return (false,null,"Not Found");
            }
            catch (Exception ex)
            {                
                _logger.LogError(ex.ToString());            
                return (false,null,ex.Message);
            }
        }
    }
}