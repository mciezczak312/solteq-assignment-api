using System;

namespace EmployeesManagement.Infrastructure.Helpers
{
    public class EmployeeNotFoundException : Exception
    {
        public EmployeeNotFoundException(string msg) : base(msg)
        {
            
        }
    }
}
