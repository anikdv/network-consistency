using Microsoft.Extensions.Configuration;

namespace ReportStorage.Repositories.Cassandra
{
    internal class CassandraProperties
    {
        private const string CONF_PATH = "ReportStorage:Cassandra";
        private const string IP_ADDRESS_SEPARATOR = ",";

        public string UserName { get; }
        public string Password { get; }
        public int Port { get; }
        public string[] IpAddresses { get; }

        public CassandraProperties(IConfiguration configuration)
        {
            UserName = configuration.GetValue<string>($"{CONF_PATH}:{nameof(UserName)}");
            Password = configuration.GetValue<string>($"{CONF_PATH}:{nameof(Password)}");
            Port = configuration.GetValue<int>($"{CONF_PATH}:{nameof(Port)}");
            var rawIpAddresses = configuration.GetValue<string>($"{CONF_PATH}:{nameof(IpAddresses)}");
            IpAddresses = rawIpAddresses.Split(IP_ADDRESS_SEPARATOR);
        }
    }
}