using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeesManagement.Core.DTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }

        public IList<string> PositionsNames { get; set; }
        public AddressDTO Address { get; set; }
        public SalaryDTO Salary { get; set; }

        public EmployeeDTO()
        {
            PositionsNames = new List<string>();
        }

    }
}
