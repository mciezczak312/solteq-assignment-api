using EmployeesManagement.API.Interfaces;
using EmployeesManagement.Core.Entities;
using EmployeesManagement.Core.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using EmployeesManagement.Core.DTO;

namespace EmployeesManagement.API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(
            IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        

        public SearchResponseDto SearchEmployees(string searchTerm, int skip, int take, string orderBy)
        {
            return _employeeRepository.SearchEmployees(searchTerm, skip, take, orderBy);
        }

        public Employee GetEmployeeById(int id)
        {
            return _employeeRepository.GetById(id);
        }

        public void DeleteEmployee(int id)
        {
            _employeeRepository.Delete(id);
        }

        public int UpsertEmployee(EmployeeDto dto)
        {
            var addressObj = _mapper.Map<Address>(dto.Address);
            var employeeObj = _mapper.Map<Employee>(dto);
            var salaryObj = _mapper.Map<Salary>(dto.Salary);
            var salaryList = new List<Salary>();

            var dateTmpFrom = salaryObj.FromDate;

            while (dateTmpFrom.AddMonths(1) <= salaryObj.ToDate)
            {   
                salaryList.Add(new Salary
                {
                    FromDate = dateTmpFrom,
                    ToDate = dateTmpFrom.AddMonths(1),
                    Amount = salaryObj.Amount
                });
                dateTmpFrom = dateTmpFrom.AddMonths(1);
            }

            if (employeeObj.Id != 0)
            {
                //UPDATE
                _employeeRepository.Update(employeeObj, addressObj, salaryList);
                return 0;
            }

            return _employeeRepository.Insert(employeeObj, addressObj, salaryList);

        }
    }
}
