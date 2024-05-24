using Ecommerce_Rookie_NashTech_CustomerFrontend.Service;

namespace Ecommerce_Rookie_NashTech_CustomerFrontend.Extensions
{
    public static class StartupExtensions
    {
        public static void AddBussinessService(this IServiceCollection services)
        {
            services.AddHttpClient<ICategoryClient, CategoryClient>();            
        }
    }
}
