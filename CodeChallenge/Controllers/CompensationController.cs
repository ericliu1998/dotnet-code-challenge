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
    [Route("api/compensation")]
    public class CompensationController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _CompensationService;

        public CompensationController(ILogger<CompensationController> logger, ICompensationService CompensationService)
        {
            _logger = logger;
            _CompensationService = CompensationService;
        }

        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Received Compensation create request for '{compensation.Employee.FirstName} {compensation.Employee.LastName}'");

            _CompensationService.Create(compensation);

            return Ok(compensation);
            //return CreatedAtRoute("getCompensationById", new { id = compensation.Id }, compensation);

        }

        [HttpGet("{id}", Name = "getCompensationByEmployeeId")]
        public IActionResult GetCompensationByEmployeeId(String id)
        {
            _logger.LogDebug($"Received Compensation get request for '{id}'");

            var Compensation = _CompensationService.GetById(id);

            if (Compensation == null)
                return NotFound();

            return Ok(Compensation);
        }

    }
}
