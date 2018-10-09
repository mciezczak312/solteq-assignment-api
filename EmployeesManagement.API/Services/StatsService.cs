using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using EmployeesManagement.API.Interfaces;
using EmployeesManagement.API.Models;
using EmployeesManagement.Core.Interfaces;

namespace EmployeesManagement.API.Services
{
    public class StatsService : IStatsService
    {
        private readonly IStatsRepository _repository;


        public StatsService(IStatsRepository repo)
        {
            _repository = repo;
        }

        public IEnumerable<AverageMonthsSalary> GetAverageMonthsSalary()
        {
            var obj = _repository.ExecuteSql(@"
                    select sum(Salary.amount) / count(distinct employee_id) as Amount,
                           EXTRACT(MONTH from fromDate)                     as MonthNumber,
                           EXTRACT(YEAR from fromDate)                      as Year
                    from Salary
                    group by EXTRACT(MONTH from fromDate), EXTRACT(YEAR from fromDate)
                    order by Year, MonthNumber;");

            return obj.Select(x => new AverageMonthsSalary()
            {
                Amount = x.Amount,
                MonthName = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName((int)x.MonthNumber),
                Year = x.Year
            }).ToList();
        }

        public EmployeesStats GetEmployeesStats()
        {
            var avgSalary = _repository.ExecuteSql(@"
                    select avg(Salary.amount) as AverageSalary
                    from Salary
                    left join Employee E on Salary.employee_id = E.id
                    where (CURRENT_DATE() between Salary.fromDate and Salary.toDate);").FirstOrDefault();

            var employeesCountResult = _repository.ExecuteSql(@"SELECT COUNT(*) as EmployeesCount from Employee").FirstOrDefault();

            if (avgSalary == null || employeesCountResult == null)
                throw new ArgumentException();

            return new EmployeesStats()
            {
                AverageCurrentSalary = avgSalary.AverageSalary,
                EmployeesCount = employeesCountResult.EmployeesCount
            };
        }
    }
}
