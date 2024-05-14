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
    public class Payment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id { get; set; }

        public Guid orderId { get; set; }

        public virtual Order Order { get; set; } = null!;

        public double amount { get; set; }

        public EPayType payType { get; set; }

        public DateTime payTime { get; set; }
    }
}
