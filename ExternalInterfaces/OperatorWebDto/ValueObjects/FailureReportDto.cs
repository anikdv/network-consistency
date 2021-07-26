using System;

namespace NetworkConsistency.ExternalInterfaces.OperatorWebDto.ValueObjects
{
    public class FailureReportDto
    {
        public Guid UID { get; init; }
        public DateTime CreationDate { get; init; }
        public DateTime? InWorkDate { get; init; }
        public DateTime? FinishDate { get; init; }
    }
}