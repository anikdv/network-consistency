using System;
using NetworkConsistency.Domain.Aggregates.Sensor;

namespace ReportStorage.Repositories.Cassandra.Models
{
    internal class CassandraSensor
    {
        public Guid UID { get; set; }
        public string Name { get; set; }
        public SensorStates State { get; set; }
        public DateTime StateDate { get; set; }

        public Guid SectionUID { get; set; }
        public string SectionName { get; set; }
        public double Longtitude { get; set; }
        public double Latitude { get; set; }

        public CassandraSensor() { }

        public CassandraSensor(Sensor sensor)
        {
            UID = sensor.UID;
            Name = sensor.Name;
            State = sensor.State;
            StateDate = sensor.StateDate;

            SectionUID = sensor.Section.UID;
            SectionName = sensor.Section.Name;
            Longtitude = sensor.Section.Longtitude;
            Latitude = sensor.Section.Latitude;
        }
    }
}