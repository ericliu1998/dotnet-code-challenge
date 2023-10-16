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
        private readonly IEmployeeService _employeeService;
        private readonly ICompensationService _compensationService;

        public CompensationController(ILogger<CompensationController> logger, IEmployeeService employeeService, ICompensationService compensationService)
        {
            _logger = logger;
            _compensationService = compensationService;
            _employeeService = employeeService;
        }

        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Received Compensation create request for {compensation.Employee.EmployeeId}");

            if (string.IsNullOrWhiteSpace(compensation.Employee.EmployeeId))
            {
                return BadRequest(new { error = "EmployeeId is missing." });
            }

            Employee employee = _employeeService.GetById(compensation.Employee.EmployeeId);
            if (employee == null)
            {
                return NotFound(new { error = $"Unable to find Employee from employeeId: {compensation.Employee.EmployeeId}." });
            }

            compensation.Employee = employee;

            Compensation returnedCompensation = _compensationService.Create(compensation);

            if (returnedCompensation == null)
            {
                return Conflict(new { error = $"Compensation already created for employeeId: {compensation.Employee.EmployeeId}" });
            }

            //return Ok(returnedCompensation);
            return CreatedAtRoute("getEmployeeById", new { id = compensation.Employee.EmployeeId }, compensation);



        }

        [HttpGet("{id}", Name = "getCompensationByEmployeeId")]
        public IActionResult GetCompensationByEmployeeId(String id)
        {
            _logger.LogDebug($"Received Compensation get request for '{id}'");

            var Compensation = _compensationService.GetById(id);

            if (Compensation == null)
                return NotFound();

            return Ok(Compensation);
        }

    }
}
