using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Core.ProductService
{
    public interface IProductService
    {
        ResultModel Create(CreateProductModel model);
        ResultModel GetAllWithSearchAndPaging(string? searchValue, int pageIndex, int pageSize);
        ResultModel GetByCategoryId(Guid categoryId, string? searchValue, int pageIndex, int pageSize);
        ResultModel GetById(Guid id);
        ResultModel Update(UpdateProductModel model);
        ResultModel Delete(Guid id);       
    }
}
