using System.Collections.Generic;

namespace NetworkConsistency.Domain.Aggregators.Sensor
{
    public class SensorEqualityComparer: IEqualityComparer<Sensor>
    {
        public bool Equals(Sensor x, Sensor y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.UID.Equals(y.UID);
        }

        public int GetHashCode(Sensor obj)
        {
            return obj.UID.GetHashCode();
        }
    }
}