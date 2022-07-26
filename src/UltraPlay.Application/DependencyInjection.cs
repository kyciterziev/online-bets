using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using UltraPlay.Application.Clients;

namespace UltraPlay.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = new RefitSettings
            {
                ContentSerializer = new XmlContentSerializer()
            };
            var ultraPlayConfig = configuration.GetSection("UltraPlay").Get<UltraPlayClientConfig>();
            services.Configure<UltraPlayClientConfig>(configuration.GetSection("UltraPlay"));
            services.AddRefitClient<IUltraPlayClient>(settings)
                .ConfigureHttpClient(o =>
                {
                    o.BaseAddress = new Uri(ultraPlayConfig.BaseUrl);
                });

            return services;
        }
    }
}