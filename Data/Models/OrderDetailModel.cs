using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class OrderDetailModel
    {
        public Guid productId { get; set; }
        public string productName { get; set; } = null!;
        public double price { get; set; }
        public int quantity { get; set; }
        public double totalPrice { get; set; }
    }

    public class CreateOrderDetailModel
    {
        public Guid orderId { get; set; }
        public Guid productId { get; set; }
        public string? description { get; set; }
        public int quantity { get; set; }
    }

    public class UpdateOrderDetailModel
    {
        public Guid id { get; set; }
        public int quantity { get; set; }
    }
}
