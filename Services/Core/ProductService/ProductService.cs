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
                    result.errorMessage = "Tên sản phẩm này đã tồn tại!";
                }
                else
                {
                    var checkCategory = _dbContext.Category.FirstOrDefault(x => x.id == model.categoryId);

                    if (checkCategory == null)
                    {
                        result.succeed = false;
                        result.errorMessage = "Không tìm thấy thông tin loại sản phẩm!";
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
            }
            catch (Exception ex)
            {
                result.errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            return result;
        }

        public ResultModel GetAllWithSearchAndPaging(string? searchValue, int pageIndex, int pageSize)
        {
            ResultModel result = new ResultModel();

            try
            {
                var listProduct = _dbContext.Product.Include(x => x.Category).OrderBy(x => x.name).ToList();

                if (!string.IsNullOrEmpty(searchValue))
                {
                    searchValue = FnUtil.RemoveVNAccents(searchValue).ToUpper();

                    listProduct = listProduct
                        .Where(x => FnUtil.RemoveVNAccents(x.name).ToUpper().Contains(searchValue) ||
                                    x.price.ToString().Contains(searchValue) ||
                                    (x.Category != null && !string.IsNullOrWhiteSpace(x.Category.name) && FnUtil.RemoveVNAccents(x.Category.name).ToUpper().Contains(searchValue))
                        ).ToList();
                }

                var listProductPaging = listProduct.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                var listDisplays = new List<ProductModel>();

                foreach (var product in listProductPaging)
                {
                    var tmp = new ProductModel
                    {
                        id = product.id,
                        name = product.name,
                        categoryId = product.categoryId,
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

        public ResultModel GetById(Guid id)
        {
            ResultModel result = new ResultModel();
            result.succeed = false;

            try
            {
                var checkProduct = _dbContext.Product.Include(x => x.Category).Where(x => x.id == id).FirstOrDefault();

                if (checkProduct == null)
                {
                    result.succeed = false;
                    result.errorMessage = "Không tìm thấy thông tin sản phẩm!";
                }
                else
                {
                    var product = new ProductModel
                    {
                        id = checkProduct.id,
                        name = checkProduct.name,
                        categoryId = checkProduct.categoryId,
                        categoryName = checkProduct.Category.name,
                        price = checkProduct.price,
                        image = checkProduct.image,
                        description = checkProduct.description,
                        createDate = checkProduct.createDate,
                        updateDate = checkProduct.updateDate,
                    };

                    result.Data = product;
                    result.succeed = true;
                }
            }
            catch (Exception ex)
            {
                result.errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            return result;
        }

        public ResultModel GetByCategoryId(Guid categoryId, string? searchValue, int pageIndex, int pageSize)
        {
            ResultModel result = new ResultModel();

            var checkCategory = _dbContext.Category.Where(x => x.id == categoryId).FirstOrDefault();

            if (checkCategory == null)
            {
                result.succeed = false;
                result.errorMessage = "Không tìm thấy thông tin loại sản phẩm!";
            }
            else
            {
                try
                {
                    var listProduct = _dbContext.Product.Where(x => x.categoryId == categoryId).OrderBy(x => x.name).ToList();

                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        searchValue = FnUtil.RemoveVNAccents(searchValue).ToUpper();

                        listProduct = listProduct
                            .Where(x => FnUtil.RemoveVNAccents(x.name).ToUpper().Contains(searchValue) ||
                                    x.price.ToString().Contains(searchValue)
                            ).ToList();
                    }

                    var listProductPaging = listProduct.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                    var listDisplay = new List<ProductModel>();

                    foreach (var product in listProductPaging)
                    {
                        var tmp = new ProductModel
                        {
                            id = product.id,
                            name = product.name,
                            categoryId = product.categoryId,
                            categoryName = product.Category.name,
                            price = product.price,
                            image = product.image,
                            description = product.description,
                            createDate = product.createDate,
                            updateDate = product.updateDate,
                        };
                        listDisplay.Add(tmp);
                    }
                    result.Data = new PagingModel()
                    {
                        Data = listDisplay,
                        total = listProduct.Count
                    };
                    result.succeed = true;

                }
                catch (Exception e)
                {
                    result.errorMessage = e.InnerException != null ? e.InnerException.Message : e.Message;
                }
            }
            return result;
        }

        public ResultModel Update(UpdateProductModel model)
        {
            ResultModel result = new ResultModel();

            try
            {
                var checkProduct = _dbContext.Product.Where(x => x.id == model.id).FirstOrDefault();

                if (checkProduct == null)
                {
                    result.succeed = false;
                    result.errorMessage = "Không tìm thấy thông tin sản phẩm !";
                }
                else
                {
                    var checkExists = _dbContext.Product.FirstOrDefault(x => x.name == model.name && x.name != checkProduct.name);

                    if (checkExists != null)
                    {
                        result.succeed = false;
                        result.errorMessage = "Tên sản phẩm đã tồn tại !";
                    }
                    
                    else
                    {
                        var checkCategory = _dbContext.Category.FirstOrDefault(x => x.id == model.categoryId);

                        if (checkCategory == null)
                        {
                            result.succeed = false;
                            result.errorMessage = "Không tìm thấy thông tin loại sản phẩm!";
                        }
                        else
                        {
                            checkProduct.name = model.name;
                            checkProduct.categoryId = model.categoryId;
                            checkProduct.price = model.price;
                            checkProduct.image = model.image;
                            checkProduct.description = model.description;
                            checkProduct.updateDate = DateTime.UtcNow.AddHours(7);

                            _dbContext.SaveChanges();

                            result.succeed = true;
                            result.Data = checkProduct.id;
                        }
                    }
                }          
            }
            catch (Exception e)
            {
                result.errorMessage = e.InnerException != null ? e.InnerException.Message : e.Message;
            }
            return result;
        }
        
        public ResultModel Delete(Guid id)
        {
            ResultModel result = new ResultModel();

            try
            {
                var checkCategory = _dbContext.Product.Where(x => x.id == id).FirstOrDefault();

                if (checkCategory == null)
                {
                    result.succeed = false;
                    result.errorMessage = "Không tìm thấy thông tin sản phẩm !";
                }
                else
                {
                    _dbContext.Product.Remove(checkCategory);
                    _dbContext.SaveChanges();

                    result.Data = checkCategory.id;
                    result.succeed = true;
                }
            }
            catch (Exception ex)
            {
                result.errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            return result;
        }     

        
    }
}
