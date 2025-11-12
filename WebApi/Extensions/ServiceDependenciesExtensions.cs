using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Service.Interfaces;
using Service.Services;

namespace WebApi.Extensions
{
    public static class ServiceDependenciesExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostService, PostService>();
        }
    }
}