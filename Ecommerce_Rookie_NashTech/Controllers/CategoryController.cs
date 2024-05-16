using Data.Models;
using Data.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Core.CategoryService;

namespace Ecommerce_Rookie_NashTech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("[action]")]
        public IActionResult Create(CreateCategoryModel model)
        {
            if (string.IsNullOrWhiteSpace(model.name)) return BadRequest("Không nhận được tên loại mặt hàng!");
            var result = _categoryService.Create(model);
            if (result.succeed) return Ok(result.Data);
            return BadRequest(new ResponeResultModel { errorMessage = result.errorMessage });
        }

        [HttpGet("[action]")]
        public IActionResult GetAllWithSearchAndPaging(string? search, int pageIndex = ConstPaging.Index, int pageSize = ConstPaging.Size)
        {
            var result = _categoryService.GetAllWithSearchAndPaging(search, pageIndex, pageSize);
            if (result.succeed) return Ok(result.Data);
            return BadRequest(new ResponeResultModel { errorMessage = result.errorMessage });
        }

        [HttpGet("[action]/{id}")]
        public IActionResult GetById(Guid id)
        {
            var result = _categoryService.GetById(id);
            if (result.succeed) return Ok(result.Data);
            return BadRequest(new ResponeResultModel { errorMessage = result.errorMessage });
        }

        [HttpPut("[action]")]
        public IActionResult Update(UpdateCategoryModel model)
        {
            if (string.IsNullOrWhiteSpace(model.name)) return BadRequest("Không nhận được tên loại mặt hàng!");
            var result = _categoryService.Update(model);
            if (result.succeed) return Ok(result.Data);
            return BadRequest(new ResponeResultModel { errorMessage = result.errorMessage });
        }

        [HttpDelete("[action]/{id}")]
        public IActionResult Delete(Guid id)
        {
            var result = _categoryService.Delete(id);
            if (result.succeed) return Ok(result.Data);
            return BadRequest(new ResponeResultModel { errorMessage = result.errorMessage });
        }
    }
}
