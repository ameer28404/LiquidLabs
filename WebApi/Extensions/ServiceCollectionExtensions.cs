using ElmahCore;
using ElmahCore.Mvc;

namespace WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddElmahLogging(this IServiceCollection services, IWebHostEnvironment env)
        {
            var logPath = Path.Combine(env.ContentRootPath, "App_Data", "ErrLogs");
            Directory.CreateDirectory(logPath);

            services.AddElmah<XmlFileErrorLog>(options =>
            {
                options.LogPath = logPath;
                options.Path = "elmah";
            });

            return services;
        }
    }
}