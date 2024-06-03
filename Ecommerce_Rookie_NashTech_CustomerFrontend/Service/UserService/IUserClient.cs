using ViewModels;

namespace Ecommerce_Rookie_NashTech_CustomerFrontend.Service.UserService
{
    public interface IUserClient
    {
        Task<(bool isSuccess, string errorMessage)> CustomersRegister(CustomersRegisterViewModel model);
        Task<(bool isSuccess, string errorMessage)> Login (LoginViewModel model);
    }
}
