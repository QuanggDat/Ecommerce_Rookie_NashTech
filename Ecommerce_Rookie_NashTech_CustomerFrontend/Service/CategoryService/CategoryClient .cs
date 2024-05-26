using Newtonsoft.Json;
using ViewModels;

namespace Ecommerce_Rookie_NashTech_CustomerFrontend.Service.CategoryService
{
    public class CategoryClient : ICategoryClient
    {
        private readonly HttpClient _httpClient;

        public CategoryClient(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("https://localhost:7115");
        }

        public async Task<ICollection<CategoryViewModel>> GetAll()
        {
            var response = await _httpClient.GetAsync("/api/Category/GetAll");

            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();

            RootCategory rootCategory = JsonConvert.DeserializeObject<RootCategory>(content);

            List<CategoryViewModel> listCategory = rootCategory.data;

            return listCategory;
        }
    }
}
