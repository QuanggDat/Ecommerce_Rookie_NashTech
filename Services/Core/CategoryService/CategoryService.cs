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

namespace Services.Core.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _dbContext;

        public CategoryService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ResultModel Create(CreateCategoryModel model)
        {
            var result = new ResultModel();
            result.succeed = false;

            try
            {
                var checkExists = _dbContext.Category.FirstOrDefault(x => x.name == model.name);

                if (checkExists != null)
                {
                    result.succeed = false;
                    result.errorMessage = "Tên loại sản phẩm này đã tồn tại !";
                }
                else
                {
                    var newCategory = new Category
                    {
                        name = model.name,
                        description = model.description,
                        createDate = DateTime.UtcNow.AddHours(7),
                    };

                    _dbContext.Category.Add(newCategory);
                    _dbContext.SaveChanges();

                    result.succeed = true;
                    result.Data = newCategory.id;
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
                var listCategory = _dbContext.Category.Include(x => x.Products).OrderBy(x => x.name).ToList();

                if (!string.IsNullOrEmpty(searchValue))
                {
                    searchValue = FnUtil.RemoveVNAccents(searchValue).ToUpper();
                    listCategory = listCategory.Where(x => FnUtil.RemoveVNAccents(x.name).ToUpper().Contains(searchValue)).ToList();
                }

                var listCategoryPaging = listCategory.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                var listDisplays = new List<CategoryModel>();

                foreach (var category in listCategoryPaging)
                {
                    var tmp = new CategoryModel
                    {
                        id = category.id,
                        name = category.name,
                        description = category.description,
                        createDate = category.createDate,
                        updateDate = category.updateDate,
                        quantityProduct = category.Products.Count,
                    };
                    listDisplays.Add(tmp);
                }

                result.Data = new PagingModel()
                {
                    Data = listDisplays,
                    total = listCategory.Count
                };
                result.succeed = true;

            }
            catch (Exception e)
            {
                result.errorMessage = e.InnerException != null ? e.InnerException.Message : e.Message;
            }
            return result;
        }

        public ResultModel GetAll()
        {
            ResultModel result = new ResultModel();

            try
            {
                var listCategory = _dbContext.Category.Include(x => x.Products).OrderBy(x => x.name).ToList();

                var listDisplays = new List<CategoryModel>();

                foreach (var category in listCategory)
                {
                    var tmp = new CategoryModel
                    {
                        id = category.id,
                        name = category.name,
                        description = category.description,
                        createDate = category.createDate,
                        updateDate = category.updateDate,
                        quantityProduct = category.Products.Count,
                    };
                    listDisplays.Add(tmp);
                }

                result.Data = new PagingModel()
                {
                    Data = listDisplays,
                    total = listCategory.Count
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
                var checkCategory = _dbContext.Category.Include(x => x.Products).Where(x => x.id == id).FirstOrDefault();

                if (checkCategory == null)
                {
                    result.succeed = false;
                    result.errorMessage = "Không tìm thấy thông tin loại sản phẩm!";
                }
                else
                {
                    var category = new CategoryModel
                    {
                        id = checkCategory.id,
                        name = checkCategory.name,
                        description = checkCategory.description,
                        createDate = checkCategory.createDate,
                        updateDate = checkCategory.updateDate,
                        quantityProduct = checkCategory.Products.Count,                 
                    };

                    result.Data = category;
                    result.succeed = true;
                }

            }
            catch (Exception ex)
            {
                result.errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            return result;
        }

        public ResultModel Update(UpdateCategoryModel model)
        {
            ResultModel result = new ResultModel();

            try
            {
                var checkCategory = _dbContext.Category.Where(x => x.id == model.id).FirstOrDefault();

                if (checkCategory == null)
                {
                    result.succeed = false;
                    result.errorMessage = "Không tìm thấy thông tin loại sản phẩm !";
                }
                else
                {
                    var checkExists = _dbContext.Category.FirstOrDefault(x => x.name == model.name && x.name != checkCategory.name);

                    if (checkExists != null)
                    {
                        result.succeed = false;
                        result.errorMessage = "Tên loại sản phẩm đã tồn tại !";
                    }
                    else
                    {
                        checkCategory.name = model.name;
                        checkCategory.description = model.description;
                        checkCategory.updateDate = DateTime.UtcNow.AddHours(7);

                        _dbContext.SaveChanges();

                        result.succeed = true;
                        result.Data = checkCategory.id;
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
                var checkCategory = _dbContext.Category.Where(x => x.id == id).FirstOrDefault();

                if (checkCategory == null)
                {
                    result.succeed = false;
                    result.errorMessage = "Không tìm thấy thông tin loại sản phẩm!";
                }
                else
                {
                    var isExistedProduct = _dbContext.Product.Any(x => x.categoryId == id);

                    if (isExistedProduct)
                    {
                        result.errorMessage = "Hãy cập nhập hoặc xoá sản phẩm trước khi xoá loại sản phẩm !";
                    }
                    else
                    {
                        _dbContext.Category.Remove(checkCategory);
                        _dbContext.SaveChanges();

                        result.Data = checkCategory.id;
                        result.succeed = true;
                    }
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
