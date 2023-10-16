using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Models
{
    public class CompensationDb
    {
        [Key] 
        public String EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int Salary { get; set; }
        public String EffectiveDate { get; set; }
    }
}
