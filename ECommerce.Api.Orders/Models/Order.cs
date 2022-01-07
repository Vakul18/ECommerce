
namespace ECommerce.Api.Orders.Models
{
    using System;
    using System.Collections.Generic;

    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDateTime { get; set; }
        public decimal Total { get; set; }
        
        public List<OrderItem> Items { get; set; }
    }
}