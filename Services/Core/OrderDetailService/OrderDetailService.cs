using Data.DataAccess;
using Data.Models;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Data.Utils;

namespace Services.Core.OrderDetailService
{
    public class OrderDetailService : IOrderDetailService
    {

        private readonly AppDbContext _dbContext;

        public OrderDetailService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ResultModel GetByOrderId(Guid orderId, int pageIndex, int pageSize, string? search = null)
        {
            ResultModel result = new ResultModel();

            var checkOrder = _dbContext.Order.Where(x => x.id == orderId).FirstOrDefault();

            if (checkOrder == null)
            {
                result.succeed = false;
                result.errorMessage = "Không tìm thấy thông tin đơn hàng!";
            }
            else
            {
                try
                {
                    var listOrderDetail = _dbContext.OrderDetail.Include(x => x.Product)
                        .Where(x => x.orderId == orderId).OrderBy(x => x.Product.name).ToList();

                    var listOrderDetailPaging = listOrderDetail.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                    var listDisplay = new List<OrderDetailModel>();

                    foreach (var orderDetail in listOrderDetailPaging)
                    {
                        var tmp = new OrderDetailModel
                        {
                            productId = orderDetail.id,
                            productName = orderDetail.Product.name,
                            imange = orderDetail.Product.image,
                            quantity = orderDetail.quantity,
                            price = orderDetail.price,
                            totalPrice = orderDetail.totalPrice,
                        };
                        listDisplay.Add(tmp);
                    }
                    result.Data = new PagingModel()
                    {
                        Data = listDisplay,
                        total = listOrderDetail.Count
                    };
                    result.succeed = true;

                }
                catch (Exception e)
                {
                    result.errorMessage = e.InnerException != null ? e.InnerException.Message : e.Message;
                }
            }
            return result;
        }      
    }
}
