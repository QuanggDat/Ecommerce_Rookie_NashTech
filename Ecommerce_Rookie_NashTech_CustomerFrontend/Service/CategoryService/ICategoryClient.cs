using ViewModels;

namespace Ecommerce_Rookie_NashTech_CustomerFrontend.Service.CategoryService
{
    public interface ICategoryClient
    {
        Task<ICollection<CategoryViewModel>> GetAll();
    }
}
