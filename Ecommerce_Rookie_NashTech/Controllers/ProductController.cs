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
            if (!ValidateCreate(model))
            {
                return BadRequest(ModelState);
            }
            var result = _productService.Create(model);
            if (result.succeed) return Ok(result.Data);
            return BadRequest(new ResponeResultModel { errorMessage = result.errorMessage });
        }

        [HttpGet("[action]")]
        public IActionResult GetAllWithSearchAndPaging(string? searchValue, int pageIndex = ConstPaging.Index, int pageSize = ConstPaging.Size)
        {
            var result = _productService.GetAllWithSearchAndPaging(searchValue, pageIndex, pageSize);
            if (result.succeed) return Ok(result.Data);
            return BadRequest(new ResponeResultModel { errorMessage = result.errorMessage });
        }

        [HttpGet("[action]")]
        public IActionResult GetByCategoryId(Guid? categoryId, string? searchValue, int pageIndex = ConstPaging.Index, int pageSize = ConstPaging.Size)
        {
            var result = _productService.GetByCategoryId(categoryId, searchValue, pageIndex, pageSize);
            if (result.succeed) return Ok(result.Data);
            return BadRequest(new ResponeResultModel { errorMessage = result.errorMessage });
        }

        [HttpGet("[action]/{id}")]
        public IActionResult GetById(Guid id)
        {
            var result = _productService.GetById(id);
            if (result.succeed) return Ok(result.Data);
            return BadRequest(new ResponeResultModel { errorMessage = result.errorMessage });
        }

        [HttpPut("[action]")]
        public IActionResult Update(UpdateProductModel model)
        {
            if (!ValidateUpdate(model))
            {
                return BadRequest(ModelState);
            }
            var result = _productService.Update(model);
            if (result.succeed) return Ok(result.Data);
            return BadRequest(new ResponeResultModel { errorMessage = result.errorMessage });
        }

        [HttpDelete("[action]/{id}")]
        public IActionResult Delete(Guid id)
        {
            var result = _productService.Delete(id);
            if (result.succeed) return Ok(result.Data);
            return BadRequest(new ResponeResultModel { errorMessage = result.errorMessage });
        }

        #region Validate
        private bool ValidateCreate(CreateProductModel model)
        {
            if (string.IsNullOrWhiteSpace(model.name))
            {
                ModelState.AddModelError(nameof(model.name),
                    $"Tên sản phẩm không được để trống !");
            }
            if (model.price <= 0)
            {
                ModelState.AddModelError(nameof(model.price),
                    $"Giá tiền không được nhỏ hơn hoặc bằng 0 !");
            }
            if (ModelState.ErrorCount > 0) return false;

            return true;
        }

        private bool ValidateUpdate(UpdateProductModel model)
        {
            if (string.IsNullOrWhiteSpace(model.name))
            {
                ModelState.AddModelError(nameof(model.name),
                    $"Tên sản phẩm không được để trống !");
            }
            if (model.price <= 0)
            {
                ModelState.AddModelError(nameof(model.price),
                    $"Giá tiền không được nhỏ hơn hoặc bằng 0 !");
            }
            if (ModelState.ErrorCount > 0) return false;

            return true;
        }
        #endregion
    }
}
