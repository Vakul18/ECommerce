
namespace ECommerce.Api.Orders.Db
{
    using Microsoft.EntityFrameworkCore;
    internal class OrdersDbContext : DbContext
    {
        internal DbSet<Order> Orders {get;set;}
        public OrdersDbContext(DbContextOptions options) : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().OwnsMany<OrderItem>(o=>o.Items);
        }

    }
}