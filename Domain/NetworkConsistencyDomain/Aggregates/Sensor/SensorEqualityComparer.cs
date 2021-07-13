using System.Collections.Generic;

namespace NetworkConsistency.Domain.Aggregates.Sensor
{
    public class SensorEqualityComparer: IEqualityComparer<Aggregates.Sensor.Sensor>
    {
        public bool Equals(Aggregates.Sensor.Sensor x, Aggregates.Sensor.Sensor y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.UID.Equals(y.UID);
        }

        public int GetHashCode(Aggregates.Sensor.Sensor obj)
        {
            return obj.UID.GetHashCode();
        }
    }
}