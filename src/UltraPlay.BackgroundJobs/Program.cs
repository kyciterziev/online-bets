using UltraPlay.BackgroundJobs;
using UltraPlay.Application;
using UltraPlay.Infrastructure;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddApplication(hostContext.Configuration);
        services.AddInfrastructure(hostContext.Configuration);

        services.AddHostedService<ScopedBackgroundService>();
        services.AddScoped<IScopedProcessingService, UltraPlayApiWorker>();
    })
    .Build();


await host.RunAsync();
