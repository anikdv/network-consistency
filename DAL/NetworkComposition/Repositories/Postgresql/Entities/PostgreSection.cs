using NetworkConsistency.DAL.NetworkComposition.Repositories.Postgresql.ValueObjects;
using NetworkConsistency.Domain.Aggregates.Section;

namespace NetworkConsistency.DAL.NetworkComposition.Repositories.Postgresql.Entities
{
    public class PostgreSection: Section
    {
        public PostgreSection(PostgreStoredSection section)
        {
            UID = section.UID;
            Name = section.Name;
            Latitude = section.Latitude;
            Longtitude = section.Longtitude;
        }
    }
}