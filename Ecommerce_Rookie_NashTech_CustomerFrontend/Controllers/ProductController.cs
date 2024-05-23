using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Rookie_NashTech_CustomerFrontend.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
