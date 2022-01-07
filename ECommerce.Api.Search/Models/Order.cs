
namespace ECommerce.Api.Search.Models
{
    using System;
    using System.Collections.Generic;

    public class Order
    {
        public int Id { get; set; }
        
        public DateTime OrderDateTime { get; set; }
        public decimal Total { get; set; }
        
        public List<OrderItem> Items { get; set; }
    }
}