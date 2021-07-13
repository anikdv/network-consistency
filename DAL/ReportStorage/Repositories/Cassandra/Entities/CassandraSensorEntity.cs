using NetworkConsistency.Domain.Aggregates.Sensor;
using ReportStorage.Repositories.Cassandra.Models;

namespace ReportStorage.Repositories.Cassandra.Entities
{
    internal class CassandraSensorEntity: Sensor
    {
        public CassandraSensorEntity(CassandraSensor storedSensor)
        {
            UID = storedSensor.UID;
            Name = storedSensor.Name;
            State = storedSensor.State;
            StateDate = storedSensor.StateDate;
            Section = new CassandraSectionEntity
            (
                storedSensor.SectionUID,
                storedSensor.SectionName,
                storedSensor.Latitude,
                storedSensor.Longtitude
            );
        }
    }
}