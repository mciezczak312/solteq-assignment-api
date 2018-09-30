using EmployeesManagement.API.Interfaces;
using EmployeesManagement.Core.Interfaces;
using System;
using EmployeesManagement.Core.Entities;

namespace EmployeesManagement.API.Services
{
    public class AddressService : IAddressService
    {
        private readonly IRepository<Address> _addressRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public AddressService(IRepository<Address> addressRepo, IEmployeeRepository employeeRepo)
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
