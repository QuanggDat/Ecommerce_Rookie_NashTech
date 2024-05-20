using Data.Enums;
using Data.Models;
using Data.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Core.OrderService;

namespace Ecommerce_Rookie_NashTech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost("[action]")]
        public IActionResult Create(CreateOrderModel model)
        {
            var result = _orderService.Create(model);
            if (result.succeed) return Ok(result.Data);
            return BadRequest(new ResponeResultModel { errorMessage = result.errorMessage });
        }

        [HttpGet("[action]/{userId}")]
        public IActionResult GetAllOrderByCustomerIdWithSearchAndPaging(Guid userId, int pageIndex = ConstPaging.Index, int pageSize = ConstPaging.Size)
        {
            var result = _orderService.GetAllOrderByCustomerIdWithSearchAndPaging(userId, pageIndex, pageSize);
            if (result.succeed) return Ok(result.Data);
            return BadRequest(new ResponeResultModel { errorMessage = result.errorMessage });
        }

        [HttpGet("[action]/{id}")]
        public IActionResult GetById(Guid id)
        {
            var result = _orderService.GetById(id);
            if (result.succeed) return Ok(result.Data);
            return BadRequest(new ResponeResultModel { errorMessage = result.errorMessage });
        }

        [HttpPut("[action]")]
        public IActionResult Update(Guid id, EOrderStatus orderStatus)
        {
            var result = _orderService.UpdateStatus(id, orderStatus);
            if (result.succeed) return Ok(result.Data);
            return BadRequest(new ResponeResultModel { errorMessage = result.errorMessage });
        }
    }
}
