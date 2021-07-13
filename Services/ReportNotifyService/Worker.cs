using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ReportStorage;

namespace NetworkConsistency.Services.ReportService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IReportRepository _reportRepository;
        private readonly INotifier _notifier;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _reportRepository = ReportRepositoryFabric.GetReportRepository(configuration);
            _notifier = NotificationFabric.GetNotifier(configuration);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);

                var failures = await _reportRepository.GetNotProcessedReports();
                if (failures.IsSuccess)
                {
                    var notificationResult = await _notifier.NotifyUsers(failures.Value);
                    if (notificationResult.IsSuccess)
                    {
                        _logger.LogInformation("Notified users about {FailureCount} failures", failures.Value.Length);
                    }
                    else
                    {
                        _logger.LogError("Error notifying users: {LoadError}", notificationResult.Errors);
                    }
                }
                else
                {
                    _logger.LogError("Error loading failure reports: {LoadError}", failures.Errors);
                }
            }
        }
    }
}