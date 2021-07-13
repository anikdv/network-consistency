using System;
using System.Threading.Tasks;
using FluentResults;
using NetworkConsistency.Domain.Aggregators.FailureReport;

namespace ReportStorage
{
    public interface IReportRepository: IDisposable
    {
        Task<Result<FailureReport[]>> GetNotProcessedReports();
        Task<Result<FailureReport[]>> GetInWorkReports();
        Task<Result<FailureReport[]>> GetFinishedReports();

        Task<Result> SaveFailureReport(FailureReport report);
    }
}