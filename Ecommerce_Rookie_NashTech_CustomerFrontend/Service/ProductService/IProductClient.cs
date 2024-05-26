using ViewModels;

namespace Ecommerce_Rookie_NashTech_CustomerFrontend.Service.ProductService
{
    public interface IProductClient
    {
        Task<ICollection<ProductViewModel>> GetByCategoryId(Guid? categoryId, string? searchValue);
        Task<ProductViewModel> GetById(Guid id);
    }
}
