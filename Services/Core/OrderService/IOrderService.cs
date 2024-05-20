using Data.Enums;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Core.OrderService
{
    public interface IOrderService
    {
        ResultModel Create(CreateOrderModel model);
        ResultModel GetAllOrderByCustomerIdWithSearchAndPaging(Guid customersId, int pageIndex, int pageSize);
        ResultModel GetById(Guid id);
        ResultModel UpdateStatus(Guid id, EOrderStatus orderStatus);
    }
}
