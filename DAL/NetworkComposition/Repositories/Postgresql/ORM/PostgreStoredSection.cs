using System;

namespace NetworkConsistency.DAL.NetworkComposition.Repositories.Postgresql.ValueObjects
{
    public class PostgreStoredSection
    {
        public Guid UID { get; protected init; }
        public string Name { get; protected init; }
        public double Latitude { get; protected init; }
        public double Longtitude { get; protected init; }
    }
}