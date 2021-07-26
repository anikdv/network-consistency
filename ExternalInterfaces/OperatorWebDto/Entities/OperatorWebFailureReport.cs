using NetworkConsistency.Domain.Aggregates.FailureReport;
using NetworkConsistency.ExternalInterfaces.OperatorWebDto.ValueObjects;

namespace NetworkConsistency.ExternalInterfaces.OperatorWebDto.Entities
{
    public class OperatorWebFailureReport: FailureReport
    {
        private OperatorWebFailureReport(FailureReportDto reportDto)
        {
            UID = reportDto.UID;
            CreationDate = reportDto.CreationDate;
            InWorkDate = reportDto.InWorkDate;
            FinishDate = reportDto.FinishDate;
        }

        public static FailureReport Create(FailureReportDto reportDto) => new OperatorWebFailureReport(reportDto);
    }
}