using System;

namespace NetworkConsistency.Domain.Aggregates.Sensor
{
    public record StateTimeSeries (Guid Uid, SensorStates State, DateTime StateDate);
}