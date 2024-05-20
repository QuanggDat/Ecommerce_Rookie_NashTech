using Data.Entities;
using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class OrderModel
    {
        public Guid id { get; set; }
        public Guid customersId { get; set; }
        public User Customers { get; set; } = null!;
        public DateTime orderDate { get; set; }
        public string receiverFullname { get; set; } = null!;
        public string receiverPhonenumber { get; set; } = null!;
        public string receiverAddress { get; set; } = null!;
        public EOrderStatus status { get; set; }
        public List<OrderDetailModel> ListOrderDetail { get; set; } = null!;
    }

    public class CreateOrderModel
    {
        public Guid customersId { get; set; }       
        public Payment Payment { get; set; } = null!;
        public List<OrderDetailModel> ListOrderDetail { get; set; } = null!;

    }

    public class UpdateOrderModel
    {
        public List<OrderDetailModel> ListOrderDetail { get; set; } = null!;
    }
}
