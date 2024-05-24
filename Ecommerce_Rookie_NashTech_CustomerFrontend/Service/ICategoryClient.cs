using ViewModels;

namespace Ecommerce_Rookie_NashTech_CustomerFrontend.Service
{
    public interface ICategoryClient
    {
        Task<ICollection<CategoryViewModel>> GetAll();
    }
}
