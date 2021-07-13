using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NetworkConsistency.DAL.NetworkComposition;
using NetworkConsistency.Domain.Aggregators.Section;
using SensorAdminApi.Model.Entities;
using SensorAdminApi.Model.ValueObjects;

namespace NetworkConsistency.ExternalInterfaces.SensorAdminApi.Controllers
{
    [ApiController, Route("/api/sections")]
    public class SensiwSectionController : Controller
    {
        private readonly INetworkCompositionRepository _networkCompositionRepository;

        public SensiwSectionController(IConfiguration configuration)
        {
            _networkCompositionRepository = NetworkCompositionFabric.GetNetworkCompositionRepository(configuration);
        }
        
        [HttpGet]
        public async Task<ActionResult<Section[]>> Get()
        {
            var sections = await _networkCompositionRepository.GetSections();
            return sections.IsSuccess
                ? sections.Value
                : StatusCode(StatusCodes.Status406NotAcceptable, sections.Errors);
        }
        [HttpPut("{uid:guid}")]
        public async Task<ActionResult<Section>> Put(Guid uid, [FromBody] ApiSectionDto sectionDto)
        {
            var section = new ApiSection(uid, sectionDto);
            var savedSection = await _networkCompositionRepository.SaveSection(section);
            return savedSection.IsSuccess
                ? savedSection.Value
                : StatusCode(StatusCodes.Status400BadRequest, savedSection.Errors);
        }
    }
}