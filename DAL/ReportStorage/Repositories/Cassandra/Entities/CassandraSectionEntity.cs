using System;
using NetworkConsistency.Domain.Aggregates.Section;

namespace ReportStorage.Repositories.Cassandra.Entities
{
    internal class CassandraSectionEntity: Section
    {
        public CassandraSectionEntity(Guid uid, string name, double latitude, double longtitude)
        {
            UID = uid;
            Name = name;
            Latitude = latitude;
            Longtitude = longtitude;
        }
    }
}