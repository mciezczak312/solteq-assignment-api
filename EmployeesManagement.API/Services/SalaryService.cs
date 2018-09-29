using EmployeesManagement.API.Interfaces;
using EmployeesManagement.Core.Entitites;
using EmployeesManagement.Infrastructure.Repositories;

namespace EmployeesManagement.API.Services
{
    public class SalaryService : ISalaryService
    {
        private SalaryRepository _salaryRepository;

        public SalaryService(SalaryRepository repository)
        {
            _salaryRepository = repository;
        }

        public Salary GetSalaryForEmployee(int employeeId)
        {
            return _salaryRepository.GetByEmployeeId(employeeId);
        }

        public void InsertSalary(Salary salary)
        {
            _salaryRepository.Insert(salary);
        }
    }
}
