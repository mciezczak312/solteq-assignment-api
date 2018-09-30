using System;

namespace EmployeesManagement.Core.Entities
{
    public class Salary : BaseEntity
    {
        public int EmployeeId { get; set; }
        public double Amount { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

    }
}
