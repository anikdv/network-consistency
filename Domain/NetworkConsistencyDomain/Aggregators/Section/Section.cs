using System;

namespace NetworkConsistency.Domain.Aggregators.Section
{
    public class Section
    {
        public Guid UID { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }
    }
}