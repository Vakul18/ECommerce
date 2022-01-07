using System;
using Xunit;
using ECommerce.Api.Products.Models.Providers;
using ECommerce.Api.Products.Models.Profiles;
using ECommerce.Api.Products.Db;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ECommerce.Api.Products.Interfaces;
using System.Threading.Tasks;
using System.Linq;

namespace ECommerce.Api.Products.Tests
{
    public class ProductsServiceTest
    {
        [Fact]
        async public Task GetProductsReturnsAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>().UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts)).Options;
            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);
            var productProfile = new ProductProfile();
            var config = new MapperConfiguration(config=>config.AddProfile(productProfile));
            var mapper = new Mapper(config);

            IProductsProvider provider = new ProductsProvider(dbContext,null,mapper);

            var products = await provider.GetProductsAsync();

            Assert.True(products.IsSuccess);
            Assert.True(products.Products.Any());
            Assert.Null(products.ErrorMessge);
        }

         [Fact]
        async public Task CheckProductId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>().UseInMemoryDatabase(nameof(CheckProductId)).Options;
            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);
            var productProfile = new ProductProfile();
            var config = new MapperConfiguration(config=>config.AddProfile(productProfile));
            var mapper = new Mapper(config);

            IProductsProvider provider = new ProductsProvider(dbContext,null,mapper);

            var products = await provider.GetProductsAsync(1);

            Assert.True(products.IsSuccess);
            Assert.NotNull(products.Product);
            Assert.True(products.Product.Id==1);
            Assert.Null(products.ErrorMessge);
        }

        [Fact]
        async public Task InvalidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>().UseInMemoryDatabase(nameof(InvalidId)).Options;
            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);
            var productProfile = new ProductProfile();
            var config = new MapperConfiguration(config=>config.AddProfile(productProfile));
            var mapper = new Mapper(config);

            IProductsProvider provider = new ProductsProvider(dbContext,null,mapper);

            var products = await provider.GetProductsAsync(-1);

            Assert.False(products.IsSuccess);
            Assert.Null(products.Product);
            Assert.NotNull(products.ErrorMessge);
        }

        private void CreateProducts(ProductsDbContext dbContext)
        {
            for(int i=1; i<=10 ; i++)
            {
                dbContext.Products.Add(new Product(){
                    Id =i,
                    Name = Guid.NewGuid().ToString(),
                    Inventory = i*10,
                    Price = (decimal)(i*20)
                });
            }
            dbContext.SaveChanges();
        }
    }
}
