using SchedulingService.Services.Interfaces;

namespace SchedulingService.Workers;

public class MainWorker : BackgroundService
{
    private readonly ILogger<MainWorker> _logger;
    private readonly IEmailService _emailService;

    public MainWorker(ILogger<MainWorker> logger, IEmailService emailService)
    {
        _logger = logger;
        _emailService = emailService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            await _emailService.SendEmailAsync("recipient@example.com", "Testing Message", "Lorem ipsum");

            await Task.Delay(1000, stoppingToken);
        }
    }
}