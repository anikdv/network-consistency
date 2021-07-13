using NetworkConsistency.DAL.NetworkComposition.Repositories.Postgresql.ValueObjects;
using NetworkConsistency.Domain.Aggregators.Sensor;

namespace NetworkConsistency.DAL.NetworkComposition.Repositories.Postgresql.Entities
{
    internal class PostgreSensor: Sensor
    {
        public PostgreSensor(PostgreStoredSensor sensor)
        {
            UID = sensor.UID;
            Name = sensor.Name;
            State = sensor.State;
            StateDate = sensor.StateDate;
            Section = new PostgreSection(sensor.Section);
        }
    }
}