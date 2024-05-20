using Data.DataAccess;
using Data.Entities;
using Data.Enums;
using Data.Models;
using Data.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Core.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _dbContext;

        public OrderService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ResultModel Create(CreateOrderModel model)
        {
            var result = new ResultModel();
            result.succeed = false;

            try
            {
                var checkCustomers = _dbContext.User.FirstOrDefault(x => x.Id == model.customersId);

                if (checkCustomers == null)
                {
                    result.succeed = false;
                    result.errorMessage = "Không tìm thấy thông tin khách hàng!";
                }
                else
                {
                    var newOrder = new Order
                    {
                        customersId = model.customersId,
                        orderDate = DateTime.UtcNow.AddHours(7),
                        status = EOrderStatus.Pending
                    };

                    _dbContext.Order.Add(newOrder);


                    double amountOrder = 0;

                    foreach (var orderDetail in model.ListOrderDetail)
                    {
                        var product = _dbContext.Product.FirstOrDefault(x => x.id == orderDetail.productId);

                        _dbContext.OrderDetail.Add(new OrderDetail
                        {
                            orderId = newOrder.id,
                            productId = orderDetail.productId,
                            price = product!.price,
                            quantity = orderDetail.quantity,
                            totalPrice = orderDetail.price * orderDetail.quantity,        
                        });

                        amountOrder =+ orderDetail.price * orderDetail.quantity;
                    }

                    if(model.Payment.payType == EPayType.Transfer)
                    {
                        model.Payment.payTime = newOrder.orderDate;
                    }

                    _dbContext.Payment.Add(new Payment
                    {
                        orderId = newOrder.id,
                        payType = model.Payment.payType,
                        amount = amountOrder,
                        payTime = model.Payment.payTime,
                    });

                    _dbContext.SaveChanges();

                    result.succeed = true;
                    result.Data = newOrder.id;
                }
            }
            catch (Exception ex)
            {
                result.errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            return result;
        }

        public ResultModel GetAllOrderByCustomerIdWithSearchAndPaging(Guid customersId, int pageIndex, int pageSize)
        {
            ResultModel result = new ResultModel();

            try
            {
                var listOrder = _dbContext.Order.Where(x => x.customersId == customersId)
                    .Include(x => x.OrderDetails).OrderByDescending(x => x.orderDate).ToList();

                var listOrderDisplay = new List<OrderModel>();

                foreach (var order in listOrder)
                {
                    var tmp = new OrderModel
                    {
                        id = order.id,
                        customersId = order.customersId,
                        status = order.status,
                        orderDate = order.orderDate,
                        receiverFullname = order.receiverFullname,
                        receiverAddress = order.receiverAddress,
                        receiverPhonenumber = order.receiverPhonenumber,

                        ListOrderDetail = order.OrderDetails.Select(x => new OrderDetailModel
                        {
                            productId = x.id,
                            productName = x.Product.name,
                            quantity = x.quantity,
                            price = x.price,
                            totalPrice = x.totalPrice,
                        }).ToList(),
                    };
                    listOrderDisplay.Add(tmp);
                }

                result.Data = new PagingModel()
                {
                    Data = listOrderDisplay,
                    total = listOrder.Count
                };
                result.succeed = true;

            }
            catch (Exception e)
            {
                result.errorMessage = e.InnerException != null ? e.InnerException.Message : e.Message;
            }
            return result;
        }

        public ResultModel GetById(Guid id)
        {
            ResultModel result = new ResultModel();

            try
            {
                var checkOrder = _dbContext.Order.Where(x => x.id == id)
                    .Include(x => x.OrderDetails).FirstOrDefault();

                if (checkOrder == null)
                {
                    result.succeed = false;
                    result.errorMessage = "Không tìm thấy thông tin đơn hàng!";
                }
                else
                {
                    var orderDisplay = new OrderModel
                    {
                        id = checkOrder.id,
                        customersId = checkOrder.customersId,
                        status = checkOrder.status,
                        orderDate = checkOrder.orderDate,
                        receiverFullname = checkOrder.receiverFullname,
                        receiverAddress = checkOrder.receiverAddress,
                        receiverPhonenumber = checkOrder.receiverPhonenumber,

                        ListOrderDetail = checkOrder.OrderDetails.Select(x => new OrderDetailModel
                        {
                            productId = x.id,
                            productName = x.Product.name,
                            quantity = x.quantity,
                            price = x.price,
                            totalPrice = x.totalPrice,
                        }).ToList(),
                    };
                    result.Data = orderDisplay;
                    result.succeed = true;
                };
            }
            catch (Exception ex)
            {
                result.errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            return result;
        }

        public ResultModel UpdateStatus(Guid id, EOrderStatus orderStatus)
        {
            var result = new ResultModel();
            try
            {
                var checkOrder = _dbContext.Order.Where(x => x.id == id)
                    .Include(x => x.OrderDetails).FirstOrDefault();

                if (checkOrder == null)
                {
                    result.succeed = false;
                    result.errorMessage = "Không tìm thấy thông tin đơn hàng!";
                }
                else
                {
                    checkOrder.status = orderStatus;

                    var payment = _dbContext.Payment.Where(x => x.orderId == id).FirstOrDefault();

                    if (orderStatus == EOrderStatus.Completed && payment!.payType == EPayType.Cash)
                    {
                        payment.payTime = DateTime.Now.AddHours(7);
                    }
                    _dbContext.SaveChanges();

                    result.Data = checkOrder.id;
                    result.succeed = true;
                }
            }
            catch (Exception ex)
            {
                result.errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            return result;
        }          
    }
}
