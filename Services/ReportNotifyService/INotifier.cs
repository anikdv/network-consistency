using System.Threading.Tasks;
using FluentResults;
using NetworkConsistency.Domain.Aggregators.FailureReport;

namespace NetworkConsistency.Services.ReportService
{
    internal interface INotifier
    {
        Task<Result> NotifyUsers(FailureReport[] report);
    }
}