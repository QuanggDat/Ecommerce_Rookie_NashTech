using Ecommerce_Rookie_NashTech_CustomerFrontend.Service.CategoryService;
using Ecommerce_Rookie_NashTech_CustomerFrontend.Service.ProductService;
using Ecommerce_Rookie_NashTech_CustomerFrontend.Service.UserService;

namespace Ecommerce_Rookie_NashTech_CustomerFrontend.Extensions
{
    public static class StartupExtensions
    {
        public static void AddBussinessService(this IServiceCollection services)
        {
            services.AddHttpClient<ICategoryClient, CategoryClient>();
            services.AddHttpClient<IProductClient, ProductClient>();
            services.AddHttpClient<IUserClient, UserClient>();
        }
    }
}
