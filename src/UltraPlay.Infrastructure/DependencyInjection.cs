using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UltraPlay.Application.Interfaces;
using UltraPlay.Infrastructure.Persistance;
using UltraPlay.Infrastructure.Services;

namespace UltraPlay.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly("UltraPlay.Api")));

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<IDomainEventService, DomainEventService>();

            return services;
        }
    }
}