using Microsoft.Extensions.Configuration;

namespace NetworkConsistency.DAL.SensorMeasurement.Repositories.InfluxDB
{
    internal class InfluxProperties
    {
        private const string CONF_PATH = "SensorManagement:Influx";
        
        public string Endpoint { get; set; }
        public string UserName { get; set; }
        public char[] Password { get; set; }

        public InfluxProperties(IConfiguration configuration)
        {
            Endpoint = configuration.GetValue<string>($"{CONF_PATH}:{nameof(Endpoint)}");
            UserName = configuration.GetValue<string>($"{CONF_PATH}:{nameof(UserName)}");
            Password = configuration.GetValue<string>($"{CONF_PATH}:{nameof(Password)}").ToCharArray();
            
        }
    }
}