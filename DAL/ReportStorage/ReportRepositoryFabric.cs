using Microsoft.Extensions.Configuration;
using ReportStorage.Repositories.Cassandra;

namespace ReportStorage
{
    public static class ReportRepositoryFabric
    {
        public static IReportRepository GetReportRepository(IConfiguration configuration) =>
            new CassandraReportRepository(new CassandraProperties(configuration));
    }
}