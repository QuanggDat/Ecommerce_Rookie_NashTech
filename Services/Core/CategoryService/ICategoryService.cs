using Data.Models;
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
        ResultModel GetAllWithSearchAndPaging(string? search, int pageIndex, int pageSize);
        ResultModel GetById(Guid id);
        ResultModel Update(UpdateCategoryModel model);
        ResultModel Delete(Guid id);
    }
}
