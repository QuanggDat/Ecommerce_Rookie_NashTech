using Newtonsoft.Json;
using ViewModels;

namespace Ecommerce_Rookie_NashTech_CustomerFrontend.Service.ProductService
{
    public class ProductClient : IProductClient
    {
        private readonly HttpClient _httpClient;

        public ProductClient(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("https://localhost:7115");
        }

        public async Task<ICollection<ProductViewModel>> GetByCategoryId(Guid? categoryId, string? searchValue)
        {
            string url = "/api/Product/GetByCategoryId";

            if (categoryId.HasValue)
            {
                url += $"?categoryId={categoryId.Value}";
            }

            if (!string.IsNullOrEmpty(searchValue))
            {
                url += $"?searchValue={searchValue}";
            }

            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();

            RootProduct rootProduct = JsonConvert.DeserializeObject<RootProduct>(content);

            List<ProductViewModel> listProduct = rootProduct.data;

            return listProduct;
        }

        public async Task<ProductViewModel> GetById(Guid id)
        {
            string url = $"/api/Product/GetById/{id}";
           
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();

            ProductViewModel product = JsonConvert.DeserializeObject<ProductViewModel>(content);

            return product;
        }
    }
}
