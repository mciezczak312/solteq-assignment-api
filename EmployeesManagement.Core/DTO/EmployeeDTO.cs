using System.Collections.Generic;

namespace EmployeesManagement.Core.DTO
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }

        public IList<int> PositionsNamesIds { get; set; }
        public AddressDTO Address { get; set; }
        public SalaryDTO Salary { get; set; }

        public EmployeeDto()
        {
            PositionsNamesIds = new List<int>();
        }

    }
}
