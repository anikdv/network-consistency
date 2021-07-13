using System;
using NetworkConsistency.Domain.Aggregates.Sensor;

namespace SensorAdminApi.Model.ValueObjects
{
    public class ApiSensorDto
    {
        public string Name { get; set; }
        public SensorStates State { get; set; }
        public DateTime StateDate { get; set; }
    }
}