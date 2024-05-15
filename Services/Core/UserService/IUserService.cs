using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Core.UserService
{
    public interface IUserService
    {
        Task<ResultModel> Login(LoginModel model);
        Task<ResultModel> RegisterCustomers(CustomersRegisterModel model);
    }
}
