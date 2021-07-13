using System;
using FluentResults;
using NetworkConsistency.Domain.Errors;

namespace NetworkConsistency.Domain.Aggregates.Sensor
{
    public abstract class Sensor
    {
        public Guid UID { get; protected init; }
        public string Name { get; protected init; }
        public Section.Section Section { get; protected set; }
        public SensorStates State { get; protected init; }
        public DateTime StateDate { get; protected init; }

        public bool IsFailed => State switch
        {
            SensorStates.ERROR => true,
            SensorStates.OFF when Section != null => true,
            _ => false
        };

        public Result<Sensor> BindSection(Section.Section sectionToBind)
        {
            if (sectionToBind == null)
            {
                return Result.Fail<Sensor>(DomainError.Create(DomainErrorTypes.CANNOT_BIND_EMPTY_SECTION));
            }
            
            if (Section != null)
            {
                return Result.Fail<Sensor>(DomainError.Create(DomainErrorTypes.SECTION_ALREADY_EXISTS));
            }

            Section = sectionToBind;
            return Result.Ok(this);
        }
    }
}