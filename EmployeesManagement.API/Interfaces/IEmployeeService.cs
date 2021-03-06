﻿using EmployeesManagement.Core.DTO;
using EmployeesManagement.Core.Entities;


namespace EmployeesManagement.API.Interfaces
{
    public interface IEmployeeService
    {
        SearchResponseDto SearchEmployees(string searchTerm,int skip, int take, string orderBy);
        Employee GetEmployeeById(int id);
        void DeleteEmployee(int id);
        int UpsertEmployee(EmployeeDto dto);
    }
}
