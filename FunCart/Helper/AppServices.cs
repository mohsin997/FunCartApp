using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using DataLayer.Interfaces;
using DataLayer.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace FunCart.Helper
{
    public class AppServices
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IRepository, RepositoryService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
