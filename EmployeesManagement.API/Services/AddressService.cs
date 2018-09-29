using EmployeesManagement.API.Interfaces;
using EmployeesManagement.Core.Entitites;
using EmployeesManagement.Core.Interfaces;
using System;

namespace EmployeesManagement.API.Services
{
    public class AddressService : IAddressService
    {
        private readonly IRepository<Address> _addressRepository;
        private readonly IRepository<Employee> _employeeRepository;

        public AddressService(IRepository<Address> addressRepo, IRepository<Employee> employeeRepo)
        {
            _addressRepository = addressRepo;
            _employeeRepository = employeeRepo;
        }

        public Address GetAddressForEmployee(int employeeId)
        {
            var employeeAddressId = this._employeeRepository.GetById(employeeId).AddressId;
            if(employeeAddressId == 0)
            {
                throw new ArgumentNullException($"No address for employee ID: {employeeId}");
            }
            return _addressRepository.GetById(employeeAddressId);
        }
    }
}
