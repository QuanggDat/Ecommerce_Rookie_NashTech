﻿using Data.Models;
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
        ResultModel GetAllCustomerslWithSearchAndPaging(int pageIndex, int pageSize, string? search = null);
        ResultModel GetById(Guid id);
        ResultModel Update(UserUpdateModel model);
        ResultModel BannedUser(Guid id);
        ResultModel UnBannedUser(Guid id);
    }
}