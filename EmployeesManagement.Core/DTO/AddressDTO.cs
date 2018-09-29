using System;
using EmployeesManagement.Core.Entitites;

namespace EmployeesManagement.Core.DTO
{
    public class AddressDTO
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        
    }
}
