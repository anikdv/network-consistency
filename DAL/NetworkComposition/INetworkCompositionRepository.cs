using System;
using System.Threading.Tasks;
using FluentResults;
using NetworkConsistency.Domain.Aggregates.Section;
using NetworkConsistency.Domain.Aggregates.Sensor;

namespace NetworkConsistency.DAL.NetworkComposition
{
    public interface INetworkCompositionRepository
    {
        Task<Result<Sensor[]>> GetSensors();
        Task<Result<Sensor>> GetSensor(Guid sensorUid);
        Task<Result<Section[]>> GetSections();
        Task<Result<Section>> GetSection(Guid sensorUid);
        Task<Result<Sensor[]>> SaveSensorStates(StateTimeSeries[] sensorStates);
        Task<Result<Sensor>> SaveSensor(Sensor sensor);
        Task<Result<Section>> SaveSection(Section sensor);
    }
}