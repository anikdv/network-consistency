using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NetworkConsistency.DAL.NetworkComposition;
using NetworkConsistency.Domain.Aggregators.Sensor;
using SensorAdminApi.Model.Entities;
using SensorAdminApi.Model.ValueObjects;

namespace NetworkConsistency.ExternalInterfaces.SensorAdminApi.Controllers
{
    [ApiController, Route("/api/sensors")]
    public class SensorController : Controller
    {
        private readonly INetworkCompositionRepository _networkCompositionRepository;

        public SensorController(IConfiguration configuration)
        {
            _networkCompositionRepository = NetworkCompositionFabric.GetNetworkCompositionRepository(configuration);
        }
        
        [HttpGet]
        public async Task<ActionResult<Sensor[]>> Get()
        {
            var sensors = await _networkCompositionRepository.GetSensors();
            return sensors.IsSuccess
                ? sensors.Value
                : StatusCode(StatusCodes.Status406NotAcceptable, sensors.Errors);
        }
        [HttpPut("{uid:guid}")]
        public async Task<ActionResult<Sensor>> Put(Guid uid, [FromBody] ApiSensorDto sensorDto)
        {
            var sensor = new ApiSensor(uid, sensorDto);
            var savedSensor = await _networkCompositionRepository.SaveSensor(sensor);
            return savedSensor.IsSuccess
                ? savedSensor.Value
                : StatusCode(StatusCodes.Status400BadRequest, savedSensor.Errors);
        }
        
        [HttpPost("{sensorUid:guid}/bind-section/{sectionUid:guid}")]
        public async Task<ActionResult<Sensor>> Put(Guid sensorUid, Guid sectionUid)
        {
            var sensor = await _networkCompositionRepository.GetSensor(sensorUid);
            if (sensor.IsFailed)
            {
                return StatusCode(StatusCodes.Status404NotFound, sensor.Errors);
            }
            var section = await _networkCompositionRepository.GetSection(sectionUid);
            if (section.IsFailed)
            {
                return StatusCode(StatusCodes.Status404NotFound, section.Errors);
            }

            var bindedSensor = sensor.Value.BindSection(section.Value);
            if (bindedSensor.IsFailed)
            {
                return StatusCode(StatusCodes.Status400BadRequest, bindedSensor.Errors);
            }

            var savedSensor = await _networkCompositionRepository.SaveSensor(bindedSensor.Value);
            return savedSensor.IsSuccess
                ? savedSensor.Value
                : StatusCode(StatusCodes.Status400BadRequest, savedSensor.Errors);
        }
    }
}