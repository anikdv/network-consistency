using Microsoft.Extensions.Configuration;
using NetworkConsistency.DAL.NetworkComposition.Repositories.Postgresql;

namespace NetworkConsistency.DAL.NetworkComposition
{
    public static class NetworkCompositionFabric
    {
        public static INetworkCompositionRepository GetNetworkCompositionRepository(IConfiguration configuration) =>
            new PostgresqlRepository(new PostgresqlProperties(configuration));
    }
}