using Ecommerce_Rookie_NashTech_CustomerFrontend.Extensions;
using Microsoft.AspNetCore.Mvc;
using ViewModels;

namespace Ecommerce_Rookie_NashTech_CustomerFrontend.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
		public IViewComponentResult Invoke()
		{
			var cart = HttpContext.Session.Get<List<OrderDetailViewModel>>(Setting.CART_KEY) ?? new List<OrderDetailViewModel>();

			return View("CartPanel", new CartModel
			{
				quantity = cart.Sum(p => p.quantity),
				total = cart.Sum(p => p.totalPrice)
			});
		}
	}
}
