using ViewModels;

namespace Ecommerce_Rookie_NashTech_CustomerFrontend.Service.UserService
{
    public class UserClient : IUserClient
    {
        private readonly HttpClient _httpClient;

        public UserClient(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("https://localhost:7115");
        }

        public async Task CustomersRegister(CustomersRegisterViewModel model)
        {
            var response = await _httpClient.GetAsync("/api/User/RegisterCustomer{model}");

            response.EnsureSuccessStatusCode();
        }
    }
}
