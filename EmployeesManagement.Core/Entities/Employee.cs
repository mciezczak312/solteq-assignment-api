using System.Collections.Generic;

namespace EmployeesManagement.Core.Entities
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        
        public int AddressId { get; set; }

        //navigation properties

        public IList<string> PositionsNames { get; set; }

        public Employee()
        {
            PositionsNames = new List<string>();
        }

    }
}
