using System;
using System.Collections.Generic;
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
                            MONTHNAME(fromDate) as Monthname, YEAR(fromDate) as Year 
                    from Salary
                    group by Date(fromDate), fromDate;");

            return obj.Select(x => new AverageMonthsSalary()
            {
                Amount = x.Amount,
                MonthName = x.Monthname,
                Year = x.Year
            }).ToList();
        }

        public EmployeesStats GetEmployeesStats()
        {
            var obj = _repository.ExecuteSql(@"
                    select avg(Salary.amount) as AverageSalary, count(distinct E.id) as EmployeesCount 
                    from Salary
                    left join Employee E on Salary.employee_id = E.id
                    where (CURRENT_DATE() between Salary.fromDate and Salary.toDate);").FirstOrDefault();

            if (obj == null)
                throw new ArgumentException();

            return new EmployeesStats()
            {
                AverageCurrentSalary = obj.AverageSalary,
                EmployeesCount = obj.EmployeesCount
            };
        }
    }
}
