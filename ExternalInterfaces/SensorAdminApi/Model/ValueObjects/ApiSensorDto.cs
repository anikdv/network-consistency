using System;
using NetworkConsistency.Domain.Aggregators.Sensor;

namespace SensorAdminApi.Model.ValueObjects
{
    public class ApiSensorDto
    {
        public string Name { get; protected init; }
        public SensorStates State { get; protected init; }
        public DateTime StateDate { get; protected init; }
    }
}