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
        ResultModel GetAllWithSearchAndPaging(string? search, int pageIndex, int pageSize);
        ResultModel GetByProductCategoryId(Guid itemCategoryId, string? search, int pageIndex, int pageSize);
        ResultModel GetById(Guid id);
        ResultModel Update(UpdateProductModel model);
        ResultModel Delete(Guid id);       
    }
}
