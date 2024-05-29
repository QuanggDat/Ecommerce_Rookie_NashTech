using Ecommerce_Rookie_NashTech_CustomerFrontend.Extensions;
using Ecommerce_Rookie_NashTech_CustomerFrontend.Service.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModels;

namespace Ecommerce_Rookie_NashTech_CustomerFrontend.Controllers
{
    public class CustomerController : Controller
    {
		private IUserClient _userClient;

		public CustomerController(IUserClient userClient)
		{
			_userClient = userClient;
		}

		[HttpGet]
		public IActionResult RegisterCustomer()
		{
			return View();
		}

		[HttpPost]
		public IActionResult RegisterCustomer(CustomersRegisterViewModel customersRegisterViewModel,IFormFile image)
        {
			if (ModelState.IsValid)
			{
				if (image != null)
				{
					customersRegisterViewModel.image = Util.UploadImage(image, "Customer");
				}

				_userClient.CustomersRegister(customersRegisterViewModel);
				return RedirectToAction("Index", "Home");
			}				
			return View();
		}

		[HttpGet]
		public IActionResult Login(string? returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}

		[Authorize]
		public async Task<IActionResult> Logout()
		{
			return View();

		}

	}
}
