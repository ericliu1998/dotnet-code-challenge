using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;

namespace CodeChallenge.Services
{
    public class ReportingStructureService : IReportingService
    {
        private readonly ILogger<ReportingStructureService> _logger;
        private readonly IEmployeeService _employeeService;

        public ReportingStructureService(ILogger<ReportingStructureService> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }


        public Reporting GetReportingStructure(string id)
        {
            Employee employee = _employeeService.GetById(id);

            if (employee == null)
            {
                return null;
            }

            Reporting reportingStructure = new Reporting()
            {
                Employee = employee,
                NumberOfReports = GetReportingStructureHelper(employee)
            };


            return reportingStructure;
        }

        private int GetReportingStructureHelper(Employee employee)
        {
            if (employee.DirectReports == null)
            {
                return 0;
            }
            
            int sum = 0;

            foreach (Employee directReport in employee.DirectReports)
            {
                //Employee directReportEmployee = _employeeService.GetById(directReport.EmployeeId);
                sum += 1 + GetReportingStructureHelper(directReport);
            }

            return sum;
        }
    }
}
