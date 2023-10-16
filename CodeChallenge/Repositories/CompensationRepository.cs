using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CodeChallenge.Data;

namespace CodeChallenge.Repositories
{
    public class CompensationRepository : ICompensationRepository
    {
        private readonly CompensationContext _compensationContext;
        private readonly ILogger<ICompensationRepository> _logger;

        public CompensationRepository(ILogger<ICompensationRepository> logger, CompensationContext compensationContext)
        {
            _compensationContext = compensationContext;
            _logger = logger;
        }

        public CompensationDb Add(CompensationDb compensation)
        {
            _compensationContext.Compensations.Add(compensation);
            return compensation;
        }

        public CompensationDb GetById(string employeeId)
        {
            return _compensationContext.Compensations.ToList().SingleOrDefault(e => e.EmployeeId == employeeId);
            //return _CompensationContext.Compensations.ToList().Where(e => e.CompensationId == id).FirstOrDefault();

        }

        public Task SaveAsync()
        {
            return _compensationContext.SaveChangesAsync();
        }

        public Compensation Remove(Compensation compensation)
        {
            return _compensationContext.Remove(compensation).Entity;
        }
    }
}
