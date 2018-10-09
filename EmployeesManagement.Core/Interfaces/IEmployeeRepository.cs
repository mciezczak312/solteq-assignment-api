using System.Collections.Generic;
using EmployeesManagement.Core.DTO;
using EmployeesManagement.Core.Entities;

namespace EmployeesManagement.Core.Interfaces
{
    public interface IEmployeeRepository
    {
        Employee GetById(int id);
        SearchResponseDto SearchEmployees(string searchTerm, int skip, int take, string orderBy);
        void Delete(int id);
        int Insert(Employee employee, Address address, IEnumerable<Salary> salaryList);
        void Update(Employee employee, Address address, IEnumerable<Salary> salaryList);

    }
}
