using Ecommerce_Rookie_NashTech_CustomerFrontend.Extensions;
using Ecommerce_Rookie_NashTech_CustomerFrontend.Service.ProductService;
using Microsoft.AspNetCore.Mvc;
using ViewModels;

namespace Ecommerce_Rookie_NashTech_CustomerFrontend.Controllers
{
    public class CartController : Controller
    {
		private IProductClient _productClient;

		public CartController(IProductClient productClient)
		{
			_productClient = productClient;
		}

		public List<OrderDetailViewModel> carts => HttpContext.Session.Get<List<OrderDetailViewModel>>(Setting.CART_KEY) ?? new List<OrderDetailViewModel>();

		public IActionResult Index()
        {
            return View(carts);
        }

		public async Task<IActionResult> AddToCart(Guid id, int quantity = 1)
		{
			var listCart = carts;

			var item = listCart.SingleOrDefault(p => p.productId == id);

			if (item == null)
			{
				ProductViewModel product = await _productClient.GetById(id);

 				item = new OrderDetailViewModel
				 {
					productId = product.id,
					productName = product.name,
					price = product.price,
					imange = product.image ?? string.Empty,
					quantity = quantity,
					totalPrice = quantity* product.price
				 };
				listCart.Add(item);
			}
			else
			{
				item.quantity += quantity;
				item.totalPrice = item.quantity * item.price;
			}

			HttpContext.Session.Set(Setting.CART_KEY, listCart);

			return RedirectToAction("Index");
		}

		public IActionResult RemoveCart(Guid id)
		{
			var listCart = carts;

			var item = listCart.SingleOrDefault(p => p.productId == id);
			if (item != null)
			{
				listCart.Remove(item);
				HttpContext.Session.Set(Setting.CART_KEY, listCart);
			}
			return RedirectToAction("Index");
		}
	}
}
