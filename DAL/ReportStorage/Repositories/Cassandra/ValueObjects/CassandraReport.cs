using System;
using NetworkConsistency.Domain.Aggregators.FailureReport;
using ReportStorage.Repositories.Cassandra.Utils;

namespace ReportStorage.Repositories.Cassandra.Models
{
    internal class CassandraReport
    {
        public Guid UID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime InWorkDate { get; set; }
        public DateTime FinishDate { get; set; }
        public CassandraSensor[] FailedSensors { get; set; }

        public CassandraReport() { }

        public CassandraReport(FailureReport report)
        {
            UID = report.UID;
            CreationDate = report.CreationDate;
            InWorkDate = report.InWorkDate ?? DefaultDate.Get();
            FinishDate = report.FinishDate ?? DefaultDate.Get();
            FailedSensors = report.FailedSensors is {Length: > 0}
                ? Array.ConvertAll(report.FailedSensors, sensor => new CassandraSensor(sensor))
                : Array.Empty<CassandraSensor>();
        }
    }
}