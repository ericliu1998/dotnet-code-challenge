using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Services;
using CodeChallenge.Models;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/reporting")]
    public class ReportingController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IReportingService _ReportingService;

        public ReportingController(ILogger<ReportingController> logger, IReportingService ReportingService)
        {
            _logger = logger;
            _ReportingService = ReportingService;
        }


        [HttpGet("{id}")]
        public IActionResult GetReportingByEmployeeId(String id)
        {
            _logger.LogDebug($"Received Reporting get request for '{id}'");

            Reporting Reporting = _ReportingService.GetReportingStructure(id);

            //if (Reporting == null)
            //    return NotFound();

            return Ok(Reporting);
        }

    }
}
