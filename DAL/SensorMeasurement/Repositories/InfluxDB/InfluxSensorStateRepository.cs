using System;
using System.Threading.Tasks;
using FluentResults;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;
using NetworkConsistency.Domain.Aggregates.Sensor;

namespace NetworkConsistency.DAL.SensorMeasurement.Repositories.InfluxDB
{
    internal class InfluxSensorStateRepository : ISensorStateRepository
    {
        private const string MEASUREMENT_NAME = nameof(SensorMeasurement);
        private const string ORG_ID = "default";
        private readonly InfluxDBClient _influx;

        public InfluxSensorStateRepository(InfluxProperties properties)
        {
            _influx = InfluxDBClientFactory.Create(properties.Endpoint, properties.UserName, properties.Password);
        }

        public async Task<Result<StateTimeSeries[]>> GetActualSensorState(Sensor[] sensorsToListen)
        {
            var readApi = _influx.GetQueryApi();
            var query = $"from {MEASUREMENT_NAME}";
            await readApi.QueryAsync(query);
            throw new NotImplementedException();
        }

        public Task<Result> SaveSensorState(StateTimeSeries[] stateTimeSeries)
        {
            using var writeApi = _influx.GetWriteApi();
            foreach (var timeSeries in stateTimeSeries)
            {
                var point = PointData
                    .Measurement(MEASUREMENT_NAME)
                    .Field(nameof(StateTimeSeries.State), Enum.GetName(timeSeries.State))
                    .Timestamp(timeSeries.StateDate, WritePrecision.S);
                writeApi.WritePoint(timeSeries.Uid.ToString(), ORG_ID, point);
            }
            return Task.FromResult(Result.Ok());
        }
    }
}