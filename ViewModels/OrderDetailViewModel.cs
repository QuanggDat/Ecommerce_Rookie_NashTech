using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class OrderDetailViewModel
    {
        public Guid productId { get; set; }
        public string productName { get; set; } = null!;
        public string imange { get; set; } = null!;
        public double price { get; set; }
        public int quantity { get; set; }
        public double totalPrice { get; set; }
    }
    public class CartModel
    {
        public int quantity { get; set; }
        public double total { get; set; }
    }
}
