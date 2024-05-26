using Ecommerce_Rookie_NashTech_CustomerFrontend.Service.ProductService;
using Microsoft.AspNetCore.Mvc;
using ViewModels;

namespace Ecommerce_Rookie_NashTech_CustomerFrontend.Controllers
{
    public class ProductController : Controller
    {
        private IProductClient _productClient;

        public ProductController(IProductClient productClient)
        {
            _productClient = productClient;
        }
        public async Task<IActionResult> IndexAsync(Guid? categoryId, string? searchValue)
        {
            return View(await _productClient.GetByCategoryId(categoryId, searchValue));
        }

        public async Task<IActionResult> ProductDetailAsync(Guid id)
        {
            return View(await _productClient.GetById(id));
        }
    }
}
