using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Core.OrderDetailService
{
    public interface IOrderDetailService
    {
        ResultModel GetByOrderId(Guid orderId, int pageIndex, int pageSize, string? search = null);
    }
}
