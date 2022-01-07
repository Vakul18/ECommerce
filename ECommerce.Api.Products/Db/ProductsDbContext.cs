
namespace ECommerce.Api.Products.Db
{
    using Microsoft.EntityFrameworkCore;

    internal class ProductsDbContext : DbContext
    {
        internal DbSet<Product> Products { get; set; }

        public ProductsDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}