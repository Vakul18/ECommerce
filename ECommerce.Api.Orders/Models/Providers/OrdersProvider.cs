
namespace ECommerce.Api.Orders.Models.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ECommerce.Api.Orders.Db;
    using ECommerce.Api.Orders.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    internal class OrdersProvider : IOrdersProvider
    {
        private readonly ILogger<OrdersProvider> _logger;
        private readonly OrdersDbContext _dbContext;
        private readonly IMapper _mapper;

        public OrdersProvider(ILogger<OrdersProvider> logger, OrdersDbContext dbContext, IMapper mapper)
        {
            this._logger = logger;
            this._dbContext = dbContext;
            this._mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (!_dbContext.Orders.Any())
            {
                _dbContext.Orders.Add(new Order()
                    {
                        Id = 1,
                        CustomerId = 1,
                        OrderDateTime = DateTime.Now,
                        Items = new List<OrderItem>()
                        {
                            new OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                            new OrderItem() { OrderId = 1, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                            new OrderItem() { OrderId = 1, ProductId = 3, Quantity = 10, UnitPrice = 10 },
                            new OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                            new OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                        },
                        Total = 100
                    });
                _dbContext.Orders.Add(new Order()
                {
                    Id = 2,
                    CustomerId = 1,
                    OrderDateTime = DateTime.Now.AddDays(-1),
                    Items = new List<OrderItem>()
                    {
                        new OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 3, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    },
                    Total = 100
                });
                _dbContext.Orders.Add(new Order()
                {
                    Id = 3,
                    CustomerId = 2,
                    OrderDateTime = DateTime.Now,
                    Items = new List<OrderItem>()
                    {
                        new OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    },
                    Total = 100
                });
                _dbContext.SaveChanges();
            }

        }

        async Task<(bool IsSuccess, Models.Order @Order, string ErrorMessage)> IOrdersProvider.GetOrderAsync(int id)
        {
             try
            {
                 var order = await _dbContext.Orders.Where(o=>o.Id== id).FirstOrDefaultAsync();
                 if(order!=null)
                 {
                     var result = _mapper.Map<Db.Order,Models.Order>(order);
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


        async Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> IOrdersProvider.GetCustomerOrdersAsync(int customerId)
        {
            try
            {
                 var orders = await _dbContext.Orders.Where(o=>o.CustomerId==customerId).ToListAsync();
                 if(orders!=null && orders.Any())
                 {
                     var result = _mapper.Map<IEnumerable<Db.Order>,IEnumerable<Models.Order>>(orders);
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