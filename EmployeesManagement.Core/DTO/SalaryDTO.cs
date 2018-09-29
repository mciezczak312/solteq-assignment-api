using System;

namespace EmployeesManagement.Core.DTO
{
    public class SalaryDTO
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public double Amount { get; set; }
    }
}
