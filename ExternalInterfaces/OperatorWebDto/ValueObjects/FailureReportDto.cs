using System;

namespace NetworkConsistency.ExternalInterfaces.OperatorWebDto.ValueObjects
{
    public class FailureReportDto
    {
        public Guid UID { get; protected init; }
        public DateTime CreationDate { get; protected init; }
        public DateTime? InWorkDate { get; protected set; }
        public DateTime? FinishDate { get; protected set; }
    }
}