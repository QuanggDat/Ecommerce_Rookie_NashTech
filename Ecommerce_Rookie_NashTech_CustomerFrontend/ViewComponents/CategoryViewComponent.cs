using Ecommerce_Rookie_NashTech_CustomerFrontend.Service;
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
        private async Task<IEnumerable<CategoryViewModel?>> GetAllCategory()
        {
            return await _categoryClient.GetAll();
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categorys = await GetAllCategory();
            return View(categorys);
        }        
    }
}
