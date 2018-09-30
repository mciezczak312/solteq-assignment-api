using System.Collections.Generic;
using EmployeesManagement.Core.Entities;


namespace EmployeesManagement.API.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetAllEmployees(int pageIndex, int pageSize);
        Employee GetEmployeeById(int id);
    }
}
