using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class OrderDetail
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id { get; set; }

        public Guid orderId { get; set; }

        public virtual Order Order { get; set; } = null!;

        public Guid productId { get; set; }

        public virtual Product Product { get; set; } = null!;   
        
        public string? description { get; set; }

        public int quantity { get; set; }

        public double totalPrice { get; set; } = 0;
    }
}
