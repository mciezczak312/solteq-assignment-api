using EmployeesManagement.Core.Entitites;

namespace EmployeesManagement.API.Interfaces
{
    public interface ISalaryService
    {
        Salary GetSalaryForEmployee(int employeeId);
        void InsertSalary(Salary salary);
    }

    
}
