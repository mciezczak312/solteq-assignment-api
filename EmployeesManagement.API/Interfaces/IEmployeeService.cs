using EmployeesManagement.Core.Entitites;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace EmployeesManagement.API.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetAllEmployees(int pageIndex, int pageSize);
        Employee GetEmployeeById(int id);
    }
}
