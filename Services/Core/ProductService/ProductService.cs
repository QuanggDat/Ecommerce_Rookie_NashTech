using Data.DataAccess;
using Data.Entities;
using Data.Models;
using Data.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Core.ProductService
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _dbContext;

        public ProductService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ResultModel Create(CreateProductModel model)
        {
            var result = new ResultModel();
            result.succeed = false;

            try
            {
                var checkExists = _dbContext.Product.FirstOrDefault(x => x.name == model.name);

                if (checkExists != null)
                {
                    result.succeed = false;
                    result.errorMessage = "Tên sản phẩm này đã tồn tại !";
                }
                else
                {
                    var newProduct = new Product
                    {
                        categoryId = model.categoryId,
                        name = model.name,                       
                        price = model.price,
                        description = model.description,
                        image = model.image,
                        createDate = DateTime.UtcNow.AddHours(7),
                    };

                    _dbContext.Product.Add(newProduct);
                    _dbContext.SaveChanges();

                    result.succeed = true;
                    result.Data = newProduct.id;
                }
            }
            catch (Exception ex)
            {
                result.errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            return result;
        }

        public ResultModel GetAllWithSearchAndPaging(string? search, int pageIndex, int pageSize)
        {
            ResultModel result = new ResultModel();

            try
            {
                var listProduct = _dbContext.Product.Include(x => x.Category).OrderBy(x => x.name).ToList();

                if (!string.IsNullOrEmpty(search))
                {
                    search = FnUtil.RemoveVNAccents(search).ToUpper();
                    listProduct = listProduct.Where(x => FnUtil.RemoveVNAccents(x.name).ToUpper().Contains(search)).ToList();
                }

                var listProductPaging = listProduct.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                var listDisplays = new List<ProductModel>();

                foreach (var product in listProductPaging)
                {
                    var tmp = new ProductModel
                    {
                        id = product.id,
                        name = product.name,
                        categoryName = product.Category.name,
                        price = product.price,
                        image = product.image,
                        description = product.description,
                        createDate = product.createDate,
                        updateDate = product.updateDate,
                    };
                    listDisplays.Add(tmp);
                }

                result.Data = new PagingModel()
                {
                    Data = listDisplays,
                    total = listProduct.Count
                };
                result.succeed = true;

            }
            catch (Exception e)
            {
                result.errorMessage = e.InnerException != null ? e.InnerException.Message : e.Message;
            }
            return result;
        }
        public ResultModel Delete(Guid id)
        {
            throw new NotImplementedException();
        }     

        public ResultModel GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public ResultModel GetByProductCategoryId(Guid itemCategoryId, string? search, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public ResultModel Update(UpdateProductModel model)
        {
            throw new NotImplementedException();
        }
    }
}
