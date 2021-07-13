using Microsoft.Extensions.Configuration;

namespace NetworkConsistency.DAL.NetworkComposition.Repositories.Postgresql
{
    internal class PostgresqlProperties
    {
        private const string CONF_PATH = "NetworkComposition:Postgresql";
        
        public string ConnectionString { get; }
        public PostgresqlProperties(IConfiguration configuration)
        {
            ConnectionString = configuration.GetValue<string>($"{CONF_PATH}:{nameof(ConnectionString)}");
        }
    }
}