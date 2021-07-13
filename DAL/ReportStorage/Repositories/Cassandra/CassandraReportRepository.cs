using System;
using System.Linq;
using System.Threading.Tasks;
using Cassandra;
using Cassandra.Mapping;
using FluentResults;
using NetworkConsistency.Domain.Aggregates.FailureReport;
using ReportStorage.Repositories.Cassandra.Entities;
using ReportStorage.Repositories.Cassandra.Models;
using ReportStorage.Repositories.Cassandra.Utils;

namespace ReportStorage.Repositories.Cassandra
{
    internal class CassandraReportRepository : IReportRepository
    {
        private const string REPORT_TABLE_NAME = "failureReport";

        private const string SENSOR_CREATION_QUERY = "create TYPE " + nameof(CassandraSensor) +
                                                     "(" +
                                                     nameof(CassandraSensor.UID) + " uuid, " +
                                                     nameof(CassandraSensor.Name) + " text, " +
                                                     nameof(CassandraSensor.State) + " text, " +
                                                     nameof(CassandraSensor.StateDate) + " timestamp, " +
                                                     nameof(CassandraSensor.SectionUID) + " uuid, " +
                                                     nameof(CassandraSensor.SectionName) + " text, " +
                                                     nameof(CassandraSensor.Longtitude) + " double, " +
                                                     nameof(CassandraSensor.Latitude) + " double" +
                                                     ")";

        private const string REPORT_CREATION_QUERY =
            "CREATE TABLE IF NOT EXISTS " + REPORT_TABLE_NAME + " " +
            "(" +
            nameof(CassandraReport.UID) + " uuid PRIMARY KEY, " +
            nameof(CassandraReport.CreationDate) + " timestamp, " +
            nameof(CassandraReport.InWorkDate) + " timestamp, " +
            nameof(CassandraReport.FinishDate) + " timestamp, " +
            nameof(CassandraReport.FailedSensors) + " list<frozen<" + nameof(CassandraSensor) + ">>" +
            ")";

        private readonly ISession _session;

        public CassandraReportRepository(CassandraProperties properties)
        {
            var sslOptions = new SSLOptions();
            var cluster = Cluster.Builder()
                .WithCredentials(properties.UserName, properties.Password)
                .WithPort(properties.Port)
                .AddContactPoints(properties.IpAddresses)
                .WithSSL(sslOptions)
                .Build();
            _session = cluster.Connect();
            _session.Execute(SENSOR_CREATION_QUERY);
            _session.Execute(REPORT_CREATION_QUERY);
        }

        public async Task<Result<FailureReport[]>> GetNotProcessedReports()
        {
            const string PROCESSED_QUERY = "SELECT * FROM " + REPORT_TABLE_NAME +
                                           " WHERE " + nameof(CassandraReport.InWorkDate) + " = ?" +
                                           " AND " + nameof(CassandraReport.FinishDate) + " = ?";
            var reports = await new Mapper(_session)
                .FetchAsync<CassandraReport>(PROCESSED_QUERY, DefaultDate.Get(), DefaultDate.Get());
            var domainReports = reports
                .Select(report => (FailureReport)new CassandraReportEntity(report))
                .ToArray();
            return Result.Ok(domainReports);
        }

        public async Task<Result<FailureReport[]>> GetInWorkReports()
        {
            const string IN_WORK_QUERY = "SELECT * FROM " + REPORT_TABLE_NAME +
                                           " WHERE " + nameof(CassandraReport.InWorkDate) + " > ?" +
                                           " AND " + nameof(CassandraReport.FinishDate) + " = ?";
            var reports = await new Mapper(_session)
                .FetchAsync<CassandraReport>(IN_WORK_QUERY, DefaultDate.Get(), DefaultDate.Get());
            var domainReports = reports
                .Select(report => (FailureReport)new CassandraReportEntity(report))
                .ToArray();
            return Result.Ok(domainReports);
        }

        public async Task<Result<FailureReport[]>> GetFinishedReports()
        {
            const string FINISHED_QUERY = "SELECT * FROM " + REPORT_TABLE_NAME +
                                           " WHERE " + nameof(CassandraReport.InWorkDate) + " > ?" +
                                           " AND " + nameof(CassandraReport.FinishDate) + " > ?";
            var reports = await new Mapper(_session)
                .FetchAsync<CassandraReport>(FINISHED_QUERY, DefaultDate.Get(), DefaultDate.Get());
            var domainReports = reports
                .Select(report => (FailureReport)new CassandraReportEntity(report))
                .ToArray();
            return Result.Ok(domainReports);
        }

        public async Task<Result> SaveFailureReport(FailureReport report)
        {
            await new Mapper(_session).UpdateAsync(new CassandraReport(report));
            return Result.Ok();
        }

        public void Dispose()
        {
            _session?.Dispose();
        }
    }
}