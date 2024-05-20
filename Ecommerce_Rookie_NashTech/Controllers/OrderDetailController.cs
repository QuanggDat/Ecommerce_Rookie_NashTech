using Data.Models;
using Data.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Core.OrderDetailService;

namespace Ecommerce_Rookie_NashTech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        [HttpGet("[action]/{orderId}")]
        public IActionResult GetAllWithSearchAndPaging(Guid orderId, int pageIndex = ConstPaging.Index, int pageSize = ConstPaging.Size)
        {
            var result = _orderDetailService.GetByOrderId(orderId, pageIndex, pageSize);
            if (result.succeed) return Ok(result.Data);
            return BadRequest(new ResponeResultModel { errorMessage = result.errorMessage });
        }
        
    }
}
