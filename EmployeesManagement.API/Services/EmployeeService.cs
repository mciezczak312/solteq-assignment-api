using EmployeesManagement.API.Interfaces;
using EmployeesManagement.Core.Entities;
using EmployeesManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesManagement.API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Position> _positionRepository;

        public EmployeeService(
            IRepository<Employee> employeeRepository,
            IRepository<Position> positionRepository)
        {
            _employeeRepository = employeeRepository;
            _positionRepository = positionRepository;
        }

        public IEnumerable<Employee> GetAllEmployees(int pageIndex, int pageSize)
        {
            var root = _employeeRepository.ListAll();
            return root.Skip(pageSize * pageIndex).Take(pageSize).ToList();
        }

        public Employee GetEmployeeById(int id)
        {
            return _employeeRepository.GetById(id);
        }
    }
}
