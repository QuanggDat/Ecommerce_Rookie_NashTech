using Data.Models;
using Data.Utils;
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
        public async Task<ActionResult> RegisterCustomer([FromBody] CustomerRegisterModel model)
        {
            if (!ValidateRegisterCustomers(model))
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.RegisterCustomer(model);
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

        [HttpGet("[action]")]
        public IActionResult GetAllCustomerslWithSearchAndPaging(string? searchValue = null, int pageIndex = ConstPaging.Index, int pageSize = ConstPaging.Size)
        {
            var result = _userService.GetAllCustomerslWithSearchAndPaging(pageIndex, pageSize, searchValue);
            if (result.succeed) return Ok(result.Data);
            return BadRequest(result.errorMessage);
        }

        [HttpGet("[action]/{id}")]
        public IActionResult GetById(Guid id)
        {
            var result = _userService.GetById(id);
            if (result.succeed) return Ok(result.Data);
            return BadRequest(new ResponeResultModel { errorMessage = result.errorMessage });
        }

        [HttpPut("[action]")]
        public IActionResult Update(UserUpdateModel model)
        {
            var result = _userService.Update(model);
            if (result.succeed) return Ok(result.Data);
            return BadRequest(new ResponeResultModel { errorMessage = result.errorMessage });
        }

        [HttpPut("[action]/{id}")/*, Authorize(Roles = "Admin")*/]
        public IActionResult BanUser(Guid id)
        {
            var result = _userService.BannedUser(id);
            if (result.succeed) return Ok(result.Data);
            return BadRequest(new ResponeResultModel { errorMessage = result.errorMessage });
        }

        [HttpPut("[action]/{id}")/*, Authorize(Roles = "Admin")*/]
        public IActionResult UnBanUser(Guid id)
        {
            var result = _userService.UnBannedUser(id);
            if (result.succeed) return Ok(result.Data);
            return BadRequest(new ResponeResultModel { errorMessage = result.errorMessage });
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

        private bool ValidateRegisterCustomers(CustomerRegisterModel model)
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
