﻿using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Core.CategoryService
{
    public interface ICategoryService
    {
        ResultModel Create(CreateCategoryModel model);
        ResultModel GetAllWithSearchAndPaging(string? searchValue, int pageIndex, int pageSize);
        ResultModel GetAll();
        ResultModel GetById(Guid id);
        ResultModel Update(UpdateCategoryModel model);
        ResultModel Delete(Guid id);
    }
}
