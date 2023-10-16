using CodeChallenge.Models;
using System;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories
{
    public interface ICompensationRepository
    {
        CompensationDb GetById(String id);
        CompensationDb Add(CompensationDb Compensation);
        Compensation Remove(Compensation Compensation);
        Task SaveAsync();
    }
}