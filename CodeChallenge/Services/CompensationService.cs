using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;

namespace CodeChallenge.Services
{
    public class CompensationService : ICompensationService
    {
        private readonly ICompensationRepository _CompensationRepository;
        private readonly ILogger<CompensationService> _logger;
        private readonly IEmployeeService _employeeService;


        public CompensationService(ILogger<CompensationService> logger, ICompensationRepository CompensationRepository, IEmployeeService employeeService)
        {
            _CompensationRepository = CompensationRepository;
            _logger = logger;
            _employeeService = employeeService;
        }

        public Compensation Create(Compensation compensation)
        {
            if (compensation != null)
            {
                CompensationDb findCompensationDb = _CompensationRepository.GetById(compensation.Employee.EmployeeId);

                if (findCompensationDb == null)
                {
                    CompensationDb compensationDb = new()
                    {
                        EmployeeId = compensation.Employee.EmployeeId,
                        Employee = compensation.Employee,
                        Salary = compensation.Salary,
                        EffectiveDate = compensation.EffectiveDate
                    };

                    _CompensationRepository.Add(compensationDb);
                    _CompensationRepository.SaveAsync().Wait();

                    return new Compensation()
                    {
                        Employee = compensation.Employee,
                        Salary = compensationDb.Salary,
                        EffectiveDate = compensationDb.EffectiveDate
                    };
                }
            }

            return null;

        }

        public Compensation GetById(string employeeId)
        {
            if (!String.IsNullOrEmpty(employeeId))
            {
                Employee employee = _employeeService.GetById(employeeId);
                if (employee != null)
                {
                    CompensationDb compensationDb = _CompensationRepository.GetById(employeeId);

                    if (compensationDb != null)
                    {
                        return new Compensation()
                        {
                            Employee = employee,
                            Salary = compensationDb.Salary,
                            EffectiveDate = compensationDb.EffectiveDate
                        };
                    }

                }

            }

            return null;
        }
    }
}
