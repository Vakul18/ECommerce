namespace ECommerce.Api.Customers.Db
{
    using Microsoft.EntityFrameworkCore;

    internal class CustomersDbContext : DbContext
    {
        internal DbSet<Customer> Customers{get; set;} 
        public CustomersDbContext(DbContextOptions options) : base(options)
        {}

    }
}