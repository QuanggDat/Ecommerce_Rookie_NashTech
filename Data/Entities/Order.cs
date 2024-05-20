using Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id { get; set; }   
        public Guid customersId { get; set; }
        public virtual User Customers { get; set; } = null!;
        public DateTime orderDate { get; set; }
        public string receiverFullname { get; set; } = null!;
        public string receiverPhonenumber { get; set; } = null!;
        public string receiverAddress { get; set; } = null!;
        public EOrderStatus status { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; } = new();
    }
}
