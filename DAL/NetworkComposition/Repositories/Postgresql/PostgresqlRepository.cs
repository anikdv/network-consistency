using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using NetworkConsistency.DAL.NetworkComposition.Repositories.Postgresql.Entities;
using NetworkConsistency.DAL.NetworkComposition.Repositories.Postgresql.ValueObjects;
using NetworkConsistency.Domain.Aggregates.Section;
using NetworkConsistency.Domain.Aggregates.Sensor;
using NetworkConsistency.Domain.Errors;

namespace NetworkConsistency.DAL.NetworkComposition.Repositories.Postgresql
{
    internal class PostgresqlRepository: INetworkCompositionRepository
    {
        private readonly PostgreDbContext _db;

        public PostgresqlRepository(PostgresqlProperties properties)
        {
            _db = new PostgreDbContext(properties);
        }

        public async Task<Result<Sensor[]>> GetSensors()
        {
            var storedSensors = await _db.Sensors.ToArrayAsync();
            var sensors = Array.ConvertAll(storedSensors, sensor => (Sensor)new PostgreSensor(sensor));
            return Result.Ok(sensors);
        }

        public async Task<Result<Sensor>> GetSensor(Guid sensorUid)
        {
            var storedSensor = await _db.Sensors.FindAsync(sensorUid);
            if (storedSensor == null)
            {
                return Result.Fail(DomainError.Create(DomainErrorTypes.SENSOR_NOT_FOUND));
            }

            Sensor sensor = new PostgreSensor(storedSensor);
            return Result.Ok(sensor);
        }

        public async Task<Result<Section[]>> GetSections()
        {
            var storedSections = await _db.Sections.ToArrayAsync();
            var sections = Array.ConvertAll(storedSections, section => (Section)new PostgreSection(section));
            return Result.Ok(sections);
        }

        public async Task<Result<Section>> GetSection(Guid sensorUid)
        {
            var storedSection = await _db.Sections.FindAsync(sensorUid);
            if (storedSection == null)
            {
                return Result.Fail(DomainError.Create(DomainErrorTypes.SECTION_NOT_FOUND));
            }

            Section section = new PostgreSection(storedSection);
            return Result.Ok(section);
        }

        public async Task<Result<Sensor[]>> SaveSensorStates(StateTimeSeries[] sensorStates)
        {
            var sensors = new HashSet<Sensor>();
            foreach (var sensorState in sensorStates)
            {
                var transaction = await _db.Database.BeginTransactionAsync();
                var storedSensor = await _db.Sensors.FindAsync(sensorState.Uid);
                var sensorFound = storedSensor != null;
                var sensorShouldBeChanged = sensorFound && storedSensor.StateDate < sensorState.StateDate;
                if (!sensorShouldBeChanged)
                {
                    await transaction.RollbackAsync();
                    continue;
                }

                storedSensor.State = sensorState.State;
                storedSensor.StateDate = sensorState.StateDate;
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
                
                sensors.Add(new PostgreSensor(storedSensor));
            }
            return Result.Ok(sensors.ToArray());
        }

        public Task<Result<Sensor>> SaveSensor(Sensor sensor)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Section>> SaveSection(Section sensor)
        {
            throw new NotImplementedException();
        }
    }
}