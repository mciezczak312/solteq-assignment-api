using System.Collections.Generic;
using EmployeesManagement.API.Models;

namespace EmployeesManagement.API.Interfaces
{
    public interface IStatsService
    {
        IEnumerable<AverageMonthsSalary> GetAverageMonthsSalary();
        EmployeesStats GetEmployeesStats();
    }
}
