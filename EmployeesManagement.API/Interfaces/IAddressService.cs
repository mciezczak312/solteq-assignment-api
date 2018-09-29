using EmployeesManagement.Core.Entitites;

namespace EmployeesManagement.API.Interfaces
{
    public interface IAddressService
    {
        Address GetAddressForEmployee(int employeeId);
    }
}
