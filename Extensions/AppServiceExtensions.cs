using itransition_task6_server.Data;
using itransition_task6_server.Services.Interfaces;
using itransition_task6_server.Services;
using itransition_task6_server.Helpers;
using Microsoft.Extensions.Configuration;

namespace itransition_task6_server.Extensions
{
    public static class AppServiceExtensions
    {
        public  static IServiceCollection AddAppServices(this IServiceCollection services,IConfiguration configuration)
        {
            var seed = PresentationSeeder.GetSeedPresentations();
            services.AddSingleton<IPresentationStorage>(new InMemoryPresentationStorage(seed));
            services.AddSingleton<IPresentationService, PresentationService>();
            services.AddSingleton<IConnectionService, ConnectionService>();
            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddSignalR();
            return services;
        }
    }
}
