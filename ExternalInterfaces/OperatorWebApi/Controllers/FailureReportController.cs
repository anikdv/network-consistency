using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetworkConsistency.Domain.Aggregates.FailureReport;
using NetworkConsistency.ExternalInterfaces.OperatorWebApi.Model.MockRepositories;
using NetworkConsistency.ExternalInterfaces.OperatorWebDto.Entities;
using NetworkConsistency.ExternalInterfaces.OperatorWebDto.ValueObjects;
using ReportStorage;

namespace NetworkConsistency.ExternalInterfaces.OperatorWebApi.Controllers
{
    [ApiController]
    [Route("/api/failure-reports")]
    public class FailureReportController : Controller
    {
        private readonly IReportRepository _repository;

        public FailureReportController()
        {
            _repository = new ReportRepository();
        }

        [HttpGet("not-processed")]
        public async Task<ActionResult<FailureReport[]>> GetNotProcessedReports()
        {
            var processedReports = await _repository.GetNotProcessedReports();
            if (processedReports.IsFailed) return StatusCode(StatusCodes.Status400BadRequest, processedReports.Errors);
            return processedReports.Value;
        }

        [HttpGet("in-work")]
        public async Task<ActionResult<FailureReport[]>> GetInWorkReports()
        {
            var inWorkReports = await _repository.GetInWorkReports();
            if (inWorkReports.IsFailed) return StatusCode(StatusCodes.Status400BadRequest, inWorkReports.Errors);
            return inWorkReports.Value;
        }

        [HttpGet("finished")]
        public async Task<ActionResult<FailureReport[]>> GetFinishedReports()
        {
            var finishedReports = await _repository.GetFinishedReports();
            if (finishedReports.IsFailed) return StatusCode(StatusCodes.Status400BadRequest, finishedReports.Errors);
            return finishedReports.Value;
        }

        [HttpPut("put-to-work")]
        public async Task<ActionResult<FailureReport>> PutToWork(FailureReportDto reportDto)
        {
            var report = OperatorWebFailureReport.Create(reportDto);
            var putToWorkResult = report.PutToWork();
            if (putToWorkResult.IsFailed) return StatusCode(StatusCodes.Status400BadRequest, putToWorkResult.Errors);
            var saveResult = await _repository.SaveFailureReport(report);
            if (saveResult.IsFailed) return StatusCode(StatusCodes.Status400BadRequest, putToWorkResult.Errors);
            return report;
        }
        
        [HttpPut("finish")]
        public async Task<ActionResult<FailureReport>> Finish(FailureReportDto reportDto)
        {
            var report = OperatorWebFailureReport.Create(reportDto);
            var finishResult = report.FinishReport();
            if (finishResult.IsFailed) return StatusCode(StatusCodes.Status400BadRequest, finishResult.Errors);
            var saveResult = await _repository.SaveFailureReport(report);
            if (saveResult.IsFailed) return StatusCode(StatusCodes.Status400BadRequest, finishResult.Errors);
            return report;
        }
    }
}