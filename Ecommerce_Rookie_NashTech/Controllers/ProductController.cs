using Data.Models;
using Data.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Core.ProductService;

namespace Ecommerce_Rookie_NashTech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("[action]")]
        public IActionResult Create(CreateProductModel model)
        {
            var result = _productService.Create(model);
            if (result.succeed) return Ok(result.Data);
            return BadRequest(new ResponeResultModel { errorMessage = result.errorMessage });
        }

        [HttpGet("[action]")]
        public IActionResult GetAllWithSearchAndPaging(string? search, int pageIndex = ConstPaging.Index, int pageSize = ConstPaging.Size)
        {
            var result = _productService.GetAllWithSearchAndPaging(search, pageIndex, pageSize);
            if (result.succeed) return Ok(result.Data);
            return BadRequest(new ResponeResultModel { errorMessage = result.errorMessage });
        }
    }
}
