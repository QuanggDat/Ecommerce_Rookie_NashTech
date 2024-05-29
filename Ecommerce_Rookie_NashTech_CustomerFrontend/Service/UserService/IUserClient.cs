using ViewModels;

namespace Ecommerce_Rookie_NashTech_CustomerFrontend.Service.UserService
{
    public interface IUserClient
    {
        Task CustomersRegister(CustomersRegisterViewModel model);
    }
}
