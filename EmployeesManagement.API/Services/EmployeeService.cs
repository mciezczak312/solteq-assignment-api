using EmployeesManagement.API.Interfaces;
using EmployeesManagement.Core.Entities;
using EmployeesManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeesManagement.Core.DTO;

namespace EmployeesManagement.API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(
            IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        

        public SearchResponseDto SearchEmployees(string searchTerm, int skip, int take, string orderBy)
        {
            return _employeeRepository.SearchEmployees(searchTerm, skip, take, orderBy);
        }

        public Employee GetEmployeeById(int id)
        {
            return _employeeRepository.GetById(id);
        }
    }
}
