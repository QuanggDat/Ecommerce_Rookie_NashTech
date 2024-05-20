using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class PaymentModel
    {
        public Guid orderId { get; set; }
        public EPayType payType { get; set; }
        public double amount { get; set; }
        public DateTime payTime { get; set; }
    }
}
