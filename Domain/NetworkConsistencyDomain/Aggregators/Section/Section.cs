using System;

namespace NetworkConsistency.Domain.Aggregators.Section
{
    public abstract class Section
    {
        public Guid UID { get; protected init; }
        public string Name { get; protected init; }
        public double Latitude { get; protected init; }
        public double Longtitude { get; protected init; }
    }
}