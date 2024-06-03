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
        public async Task<IActionResult> RegisterCustomer(CustomersRegisterViewModel customersRegisterViewModel, IFormFile? image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    customersRegisterViewModel.image = Util.UploadImage(image, "Customer");
                }

                var (isSuccess, errorMessage) = await _userClient.CustomersRegister(customersRegisterViewModel);

                if (isSuccess)
                {
                    return RedirectToAction("Login", "Customer"); 
                }
                else
                {
                    ModelState.AddModelError("", errorMessage);
                }
            }

            return View(customersRegisterViewModel);
        }

        [HttpGet]
		public IActionResult Login(string? returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login (LoginViewModel model, string? returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;

			if (ModelState.IsValid)
			{
				var (isSuccess, errorMessage) = await _userClient.Login(model);

				if (Url.IsLocalUrl(returnUrl))
				{
					if (isSuccess)
					{
						return Redirect(returnUrl);
					}
					else
					{
						ModelState.AddModelError("", errorMessage);
					}
				}
				else
				{
					return RedirectToAction("Index", "Home");
				}
			}
			return View();
		}

		[Authorize]
		public async Task<IActionResult> Logout()
		{
			return View();

		}
	}
}
