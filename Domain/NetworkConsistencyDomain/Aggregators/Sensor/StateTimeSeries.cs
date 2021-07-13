using System;

namespace NetworkConsistency.Domain.Aggregators.Sensor
{
    public record StateTimeSeries (Guid Uid, SensorStates State, DateTime StateDate);
}