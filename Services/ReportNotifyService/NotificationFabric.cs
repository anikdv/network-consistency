using Microsoft.Extensions.Configuration;
using NetworkConsistency.Services.ReportService.Notifiers;

namespace NetworkConsistency.Services.ReportService
{
    internal static class NotificationFabric
    {
        public static INotifier GetNotifier(IConfiguration configuration) => new EmailNotifier(configuration);
    }
}