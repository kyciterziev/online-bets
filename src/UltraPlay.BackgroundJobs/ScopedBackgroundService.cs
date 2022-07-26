namespace UltraPlay.BackgroundJobs;

public class ScopedBackgroundService : BackgroundService
{
    private readonly ILogger<ScopedBackgroundService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public ScopedBackgroundService(ILogger<ScopedBackgroundService> logger, IServiceProvider serviceProvider) =>
    (_serviceProvider, _logger) = (serviceProvider, logger);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await DoWorkAsync(stoppingToken);
        }
    }

    private async Task DoWorkAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation(
            $"{nameof(ScopedBackgroundService)} is working.");

        using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            IScopedProcessingService scopedProcessingService =
                scope.ServiceProvider.GetRequiredService<IScopedProcessingService>();

            await scopedProcessingService.DoWorkAsync(stoppingToken);
        }
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation(
            $"{nameof(ScopedBackgroundService)} is stopping.");

        await base.StopAsync(stoppingToken);
    }
}
