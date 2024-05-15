using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Core.UserService;

namespace Ecommerce_Rookie_NashTech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("[action]")/*, Authorize(Roles = "Customers")*/]
        public async Task<ActionResult> RegisterCustomers([FromBody] CustomersRegisterModel model)
        {
            if (!ValidateRegisterCustomers(model))
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.RegisterCustomers(model);
            if (result.succeed) return Ok(result.Data);
            return BadRequest(new ResponeResultModel {errorMessage = result.errorMessage });
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Login([FromBody] LoginModel model)
        {
            if (!ValidateLogin(model))
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.Login(model);
            if (result.succeed) return Ok(result.Data);
            return BadRequest(new ResponeResultModel {errorMessage = result.errorMessage });
        }

        #region Validate
        private bool ValidateLogin(LoginModel model)
        {
            if (string.IsNullOrWhiteSpace(model.emailOrPhoneNumber))
            {
                ModelState.AddModelError(nameof(model.emailOrPhoneNumber),
                    $"Email/Số điện thoại không được để trống !");
            }
            if (model.password.Length < 6)
            {
                ModelState.AddModelError(nameof(model.password),
                    $"Mật khẩu phải từ 6 ký tự trở lên !");
            }
            if (ModelState.ErrorCount > 0) return false;

            return true;
        }

        private bool ValidateRegisterCustomers(CustomersRegisterModel model)
        {
            if (string.IsNullOrWhiteSpace(model.email))
            {
                ModelState.AddModelError(nameof(model.email),
                    $"Email không được để trống !");
            }
            if (string.IsNullOrWhiteSpace(model.phoneNumber))
            {
                ModelState.AddModelError(nameof(model.phoneNumber),
                    $"Số điện không được để trống !");
            }
            if (string.IsNullOrWhiteSpace(model.fullName))
            {
                ModelState.AddModelError(nameof(model.fullName),
                    $"Họ tên không được để trống !");
            }
            if (model.password.Length < 6)
            {
                ModelState.AddModelError(nameof(model.password),
                    $"Mật khẩu phải nhiều hơn 6 ký tự !");
            }
            if (ModelState.ErrorCount > 0) return false;

            return true;
        }
        #endregion
    }
}
