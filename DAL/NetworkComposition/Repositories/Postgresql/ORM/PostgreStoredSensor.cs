using System;
using NetworkConsistency.Domain.Aggregators.Section;
using NetworkConsistency.Domain.Aggregators.Sensor;

namespace NetworkConsistency.DAL.NetworkComposition.Repositories.Postgresql.ValueObjects
{
    public class PostgreStoredSensor
    {
        public Guid UID { get; set; }
        public string Name { get; set; }
        public PostgreStoredSection Section { get; set; }
        public SensorStates State { get; set; }
        public DateTime StateDate { get; set; }
    }
}