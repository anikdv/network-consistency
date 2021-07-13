using System;
using NetworkConsistency.Domain.Aggregators.FailureReport;
using ReportStorage.Repositories.Cassandra.Models;
using ReportStorage.Repositories.Cassandra.Utils;

namespace ReportStorage.Repositories.Cassandra.Entities
{
    internal class CassandraReportEntity : FailureReport
    {
        public CassandraReportEntity(CassandraReport storedReport)
        {
            UID = storedReport.UID;
            CreationDate = storedReport.CreationDate;
            InWorkDate = storedReport.InWorkDate == DefaultDate.Get()
                ? null
                : storedReport.InWorkDate;
            FinishDate = storedReport.FinishDate == DefaultDate.Get()
                ? null
                : storedReport.FinishDate;
            FailedSensors = Array.ConvertAll(storedReport.FailedSensors, sensor => new CassandraSensorEntity(sensor));
        }
    }
}