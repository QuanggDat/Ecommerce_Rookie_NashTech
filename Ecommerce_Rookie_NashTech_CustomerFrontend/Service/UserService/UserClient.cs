using Newtonsoft.Json;
using System.Text;
using ViewModels;
using static ViewModels.ResponseViewModel;

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

        public async Task<(bool isSuccess, string errorMessage)> CustomersRegister(CustomersRegisterViewModel model)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/User/RegisterCustomer", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return (true, null);
            }

            var errorContent = await response.Content.ReadAsStringAsync();

            try
            {
                // Attempt to deserialize the error content into ErrorResponse object
                var errorResponse = JsonConvert.DeserializeObject<ErrorResponseViewModel>(errorContent);
                if (errorResponse != null && !string.IsNullOrEmpty(errorResponse.errorMessage))
                {
                    return (false, errorResponse.errorMessage);
                }
            }
            catch (JsonException)
            {
                // Handle deserialization failure or unexpected format
            }

            // If unable to deserialize into ErrorResponse or no error message found, return a generic error
            return (false, "Failed to register customer");
        }

        public async Task<(bool isSuccess, string errorMessage)> Login(LoginViewModel model)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/User/Login", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return (true, null);
            }

            var errorContent = await response.Content.ReadAsStringAsync();

            try
            {
                // Attempt to deserialize the error content into ErrorResponse object
                var errorResponse = JsonConvert.DeserializeObject<ErrorResponseViewModel>(errorContent);
                if (errorResponse != null && !string.IsNullOrEmpty(errorResponse.errorMessage))
                {
                    return (false, errorResponse.errorMessage);
                }
            }
            catch (JsonException)
            {
                // Handle deserialization failure or unexpected format
            }

            // If unable to deserialize into ErrorResponse or no error message found, return a generic error
            return (false, "Failed to register customer");
        }
    }
}
