using System.Threading.Tasks;
using FluentResults;
using NetworkConsistency.Domain.Aggregates.Sensor;

namespace NetworkConsistency.DAL.SensorMeasurement
{
    public interface ISensorStateRepository
    {
        Task<Result<StateTimeSeries[]>> GetActualSensorState(Sensor[] sensorsToListen);
        Task<Result> SaveSensorState(StateTimeSeries[] stateTimeSeries);
    }
}