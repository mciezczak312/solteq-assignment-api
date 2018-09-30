using EmployeesManagement.Core.Entities;

namespace EmployeesManagement.API.Interfaces
{
    public interface IAddressService
    {
        Address GetAddressForEmployee(int employeeId);
    }
}
