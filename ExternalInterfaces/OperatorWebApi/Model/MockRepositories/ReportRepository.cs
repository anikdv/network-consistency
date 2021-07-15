using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using NetworkConsistency.Domain.Aggregates.FailureReport;
using NetworkConsistency.ExternalInterfaces.OperatorWebApi.Model.MockObjects;
using ReportStorage;

namespace NetworkConsistency.ExternalInterfaces.OperatorWebApi.Model.MockRepositories
{
    public class ReportRepository : IReportRepository
    {
        private static readonly Dictionary<Guid, FailureReport> _repository = new();

        static ReportRepository()
        {
            AppendMockReport(DateTime.Parse("2021-01-01 20:10:59"), 5);
            AppendMockReport(DateTime.Parse("2021-02-01 00:59:57"), 3);
            AppendMockReport(DateTime.Parse("2021-01-03 10:37:18"), 2);
            AppendMockReport(DateTime.Parse("2021-01-04 16:15:09"), 14);
            AppendMockReport(DateTime.Parse("2021-05-01 09:42:14"), 8);
            AppendMockReport(DateTime.Parse("2021-06-07 12:00:09"), 9);
        }

        private static void AppendMockReport(DateTime creationDate, int sensorCount)
        {
            var mockReport = new MockFailureReport(creationDate, sensorCount);
            _repository.Add(mockReport.UID, mockReport);
        }

        public void Dispose()
        {
        }

        public Task<Result<FailureReport[]>> GetNotProcessedReports()
        {
            var processedReports = _repository.Values
                .Where(report => !(report.InWorkDate.HasValue && report.FinishDate.HasValue))
                .ToArray();
            return Task.FromResult(Result.Ok(processedReports));
        }

        public Task<Result<FailureReport[]>> GetInWorkReports()
        {
            var inWorkReports = _repository.Values
                .Where(report => report.InWorkDate.HasValue && !report.FinishDate.HasValue)
                .ToArray();
            return Task.FromResult(Result.Ok(inWorkReports));
        }

        public Task<Result<FailureReport[]>> GetFinishedReports()
        {
            var finishedReports = _repository.Values
                .Where(report => report.FinishDate.HasValue)
                .ToArray();
            return Task.FromResult(Result.Ok(finishedReports));
        }

        public Task<Result> SaveFailureReport(FailureReport report)
        {
            if (!_repository.ContainsKey(report.UID))
            {
                return Task.FromResult(Result.Fail("Report does not exists"));
            }

            _repository[report.UID] = report;
            return Task.FromResult(Result.Ok());
        }
    }
}