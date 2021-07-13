using System;
using NetworkConsistency.Domain.Aggregates.Section;
using SensorAdminApi.Model.ValueObjects;

namespace SensorAdminApi.Model.Entities
{
    internal class ApiSection: Section
    {
        public ApiSection(Guid sectionUID, ApiSectionDto dto)
        {
            UID = sectionUID;
            Name = dto.Name;
            Latitude = dto.Latitude;
            Longtitude = dto.Longtitude;
        }
    }
}