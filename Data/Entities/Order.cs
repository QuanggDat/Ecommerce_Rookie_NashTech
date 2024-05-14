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

        public DateTime orderDate { get; set; }

        public Guid userId { get; set; }

        public virtual User User { get; set; } = null!;

        public EOrderStatus status { get; set; }

        public virtual List<OrderDetail> OrderDetails { get; set; } = new();
    }
}
