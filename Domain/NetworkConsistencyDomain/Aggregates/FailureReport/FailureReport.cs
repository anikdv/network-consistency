using System;
using FluentResults;
using NetworkConsistency.Domain.Errors;

namespace NetworkConsistency.Domain.Aggregates.FailureReport
{
    public class FailureReport
    {
        public Guid UID { get; protected init; }
        public DateTime CreationDate { get; protected init; }
        public DateTime? InWorkDate { get; protected set; }
        public DateTime? FinishDate { get; protected set; }
        public Sensor.Sensor[] FailedSensors { get; protected init; }

        public Result PutToWork()
        {
            if (InWorkDate.HasValue)
            {
                return Result.Fail(DomainError.Create(DomainErrorTypes.REPORT_IS_IN_WORK));
            }
            if (FinishDate.HasValue)
            {
                return Result.Fail(DomainError.Create(DomainErrorTypes.FINISHDATE_IS_SET));
            }

            InWorkDate = DateTime.Now;
            return Result.Ok();
        }
        
        public Result FinishReport()
        {
            if (FinishDate.HasValue)
            {
                return Result.Fail(DomainError.Create(DomainErrorTypes.FINISHDATE_IS_SET));
            }

            FinishDate = DateTime.Now;
            return Result.Ok();
        }

        protected FailureReport() { }
        
        public static Result<FailureReport> Create(Sensor.Sensor[] sensors)
        {
            var failedSensors = Array.FindAll(sensors, sensor => sensor.IsFailed);
            if (failedSensors.Length == 0)
            {
                return Result.Fail<FailureReport>(DomainError.Create(DomainErrorTypes.NO_FAILED_SENSORS));
            }

            var report = new FailureReport
            {
                UID = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                FailedSensors = failedSensors
            };
            return Result.Ok(report);
        }
    }
}