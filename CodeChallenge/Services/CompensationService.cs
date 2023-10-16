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
            Employee employee = _employeeService.GetById(compensation.Employee.EmployeeId);
            if ( employee != null)
            {
                if (compensation != null)
                {
                    CompensationDb compensationDb = new()
                    {
                        EmployeeId = employee.EmployeeId,
                        Employee = employee,
                        Salary = compensation.Salary,
                        EffectiveDate = compensation.EffectiveDate
                    };

                    _CompensationRepository.Add(compensationDb);
                    _CompensationRepository.SaveAsync().Wait();

                    return new Compensation()
                    {
                        Employee = _employeeService.GetById(compensationDb.EmployeeId),
                        Salary = compensationDb.Salary,
                        EffectiveDate = compensationDb.EffectiveDate
                    };

                    //compensation.Employee = employee;

                    //_CompensationRepository.Add(compensation);
                    //_CompensationRepository.SaveAsync().Wait();


                    //return compensation;
                }
            }

            return null;

        }

        public Compensation GetById(string employeeId)
        {
            if(!String.IsNullOrEmpty(employeeId))
            {
                Employee employee = _employeeService.GetById(employeeId);
                if ( employee != null)
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

                    //Compensation compensation = _CompensationRepository.GetById(employeeId);

                    //if (compensation != null)
                    //{
                    //    return compensation;
                    //}

                }

            }

            return null;
        }
    }
}
