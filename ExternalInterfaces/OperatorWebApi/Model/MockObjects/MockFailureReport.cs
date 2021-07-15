using System;
using NetworkConsistency.Domain.Aggregates.FailureReport;
using NetworkConsistency.Domain.Aggregates.Sensor;

namespace NetworkConsistency.ExternalInterfaces.OperatorWebApi.Model.MockObjects
{
    public class MockFailureReport : FailureReport
    {
        public MockFailureReport (DateTime creationDate, int sensorCount)
        {
            UID = Guid.NewGuid();
            CreationDate = creationDate;
            FailedSensors = new Sensor[sensorCount];
        }
    }
}