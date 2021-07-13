using System;
using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetworkConsistency.DAL.NetworkComposition;
using NetworkConsistency.DAL.SensorMeasurement;
using NetworkConsistency.Domain.Aggregators.FailureReport;
using NetworkConsistency.Domain.Aggregators.Sensor;
using ReportStorage;

namespace NetworkConsistency.Services.SensorService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly INetworkCompositionRepository _networkCompositionRepository;
        private readonly IReportRepository _reportRepository;
        private readonly ISensorStateRepository _sensorStateRepository;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _networkCompositionRepository = NetworkCompositionFabric.GetNetworkCompositionRepository(configuration);
            _reportRepository = ReportRepositoryFabric.GetReportRepository(configuration);
            _sensorStateRepository = SensorStateRepositoryFabric.GetSensorStateRepository(configuration);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at: {Time}", DateTimeOffset.Now);
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);

                var sensors = await _networkCompositionRepository.GetSensors();
                if (sensors.IsFailed)
                {
                    _logger.LogError("Error loading sensors from composition repository: {@Error}", sensors.Errors);
                    continue;
                }

                var sensorStates = await _sensorStateRepository.GetActualSensorState(sensors.Value);
                if (sensorStates.IsFailed)
                {
                    _logger.LogError("Error getting actual state of sensors: {@Error}", sensorStates.Errors);
                    continue;
                }

                var savedSensors = await _networkCompositionRepository.SaveSensorStates(sensorStates.Value);
                if (savedSensors.IsFailed)
                {
                    _logger.LogError("Unebale to save new sensor states due to error: {@Error}",
                        savedSensors.Errors);
                }
                await GenerateFailureReport(savedSensors);
            }
        }

        private async Task GenerateFailureReport(Result<Sensor[]> updatedSensors)
        {
            var failureReport = FailureReport.Create(updatedSensors.Value);
            if (failureReport.IsSuccess)
            {
                await _reportRepository.SaveFailureReport(failureReport.Value);
            }
        }
    }
}