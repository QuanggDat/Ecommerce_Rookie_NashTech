using Ecommerce_Rookie_NashTech_CustomerFrontend.Service.CategoryService;
using Microsoft.AspNetCore.Mvc;
using ViewModels;

namespace Ecommerce_Rookie_NashTech_CustomerFrontend.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly ICategoryClient _categoryClient;

        public CategoryViewComponent(ICategoryClient categoryClient)
        {
            _categoryClient = categoryClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _categoryClient.GetAll());
        }        
    }
}
