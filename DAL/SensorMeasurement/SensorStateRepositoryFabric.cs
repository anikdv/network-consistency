using Microsoft.Extensions.Configuration;
using NetworkConsistency.DAL.SensorMeasurement.Repositories.InfluxDB;

namespace NetworkConsistency.DAL.SensorMeasurement
{
    public static class SensorStateRepositoryFabric
    {
        public static ISensorStateRepository GetSensorStateRepository(IConfiguration configuration)
        {
            var properties = new InfluxProperties(configuration);
            var repository = new InfluxSensorStateRepository(properties);
            return repository;
        }
    }
}