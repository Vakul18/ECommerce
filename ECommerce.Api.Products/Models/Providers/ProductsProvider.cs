
namespace ECommerce.Api.Products.Models.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ECommerce.Api.Products.Db;
    using ECommerce.Api.Products.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    internal class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext _dbContext;
        private readonly ILogger<ProductsProvider> _logger;
        private readonly IMapper _mapper;

        public ProductsProvider(ProductsDbContext dbContext, ILogger<ProductsProvider> logger,IMapper mapper)
        {
            this._dbContext = dbContext;
            this._logger = logger;
            this._mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if(!_dbContext.Products.Any())
            {
                _dbContext.Products.Add(new Db.Product(){Id = 1, Name = "Keyboard",Price=20, Inventory=100});
                _dbContext.Products.Add(new Db.Product(){Id = 2, Name = "Mouse",Price=10, Inventory=110});
                _dbContext.Products.Add(new Db.Product(){Id = 3, Name = "Printer",Price=100, Inventory=10});
                _dbContext.Products.Add(new Db.Product(){Id = 4, Name = "Glass",Price=150, Inventory=180});
                _dbContext.SaveChanges();
            }
        }

        async Task<(bool IsSuccess, IEnumerable<Models.Product> Products, string ErrorMessge)> IProductsProvider.GetProductsAsync()
        {
            try
            {
                 var products = await _dbContext.Products.ToListAsync();
                 if(products!=null && products.Any())
                 {
                     var result = _mapper.Map<IEnumerable<Db.Product>,IEnumerable<Models.Product>>(products);
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

        async Task<(bool IsSuccess, Models.Product Product, string ErrorMessge)> IProductsProvider.GetProductsAsync(int id)
        {
            try
            {
                 var product = await _dbContext.Products.FirstOrDefaultAsync(p=>p.Id==id);
                 if(product!=null)
                 {
                     var result = _mapper.Map<Db.Product,Models.Product>(product);
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