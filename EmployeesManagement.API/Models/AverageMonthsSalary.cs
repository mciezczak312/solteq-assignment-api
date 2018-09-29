using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesManagement.API.Models
{
    public class AverageMonthsSalary
    {
        public double Amount { get; set; }
        public string MonthName { get; set; }
        public long Year { get; set; }
    }
}
