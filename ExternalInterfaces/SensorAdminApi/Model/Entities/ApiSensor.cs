using System;
using NetworkConsistency.Domain.Aggregates.Sensor;
using SensorAdminApi.Model.ValueObjects;

namespace SensorAdminApi.Model.Entities
{
    internal class ApiSensor: Sensor
    {
        public ApiSensor(Guid sensorUID, ApiSensorDto dto)
        {
            UID = sensorUID;
            Name = dto.Name;
            State = dto.State;
            StateDate = dto.StateDate;
            Section = null;
        }
    }
}