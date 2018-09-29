using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeesManagement.API.Models;

namespace EmployeesManagement.API.Interfaces
{
    public interface IStatsService
    {
        IEnumerable<AverageMonthsSalary> GetAverageMonthsSalary();
        EmployeesStats GetEmployeesStats();
    }
}
