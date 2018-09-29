using System;

namespace EmployeesManagement.Core.Entitites
{
    public class Salary : BaseEntity
    {
        public int EmployeeId { get; set; }
        public double Amount { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

    }
}
