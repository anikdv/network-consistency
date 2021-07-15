using NetworkConsistency.Domain.Aggregates.FailureReport;
using NetworkConsistency.ExternalInterfaces.OperatorWebDto.ValueObjects;

namespace NetworkConsistency.ExternalInterfaces.OperatorWebDto.Entities
{
    public class OperatorWebFailureReport: FailureReport
    {
        public OperatorWebFailureReport(FailureReportDto reportDto)
        {
            UID = reportDto.UID;
            CreationDate = reportDto.CreationDate;
            InWorkDate = reportDto.InWorkDate;
            FinishDate = reportDto.FinishDate;
        }
    }
}