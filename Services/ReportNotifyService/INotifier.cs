using System.Threading.Tasks;
using FluentResults;
using NetworkConsistency.Domain.Aggregates.FailureReport;

namespace NetworkConsistency.Services.ReportService
{
    internal interface INotifier
    {
        Task<Result> NotifyUsers(FailureReport[] report);
    }
}