
namespace ECommerce.Api.Orders.Db
{
    using System;
    using System.Collections.Generic;

    internal class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDateTime { get; set; }
        public decimal Total { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}