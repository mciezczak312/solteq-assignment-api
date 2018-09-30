using System;
using System.Collections.Generic;
using System.Text;
using EmployeesManagement.Core.DTO;
using EmployeesManagement.Core.Entities;

namespace EmployeesManagement.Core.Interfaces
{
    public interface IEmployeeRepository
    {
        Employee GetById(int id);
        SearchResponseDto SearchEmployees(string searchTerm, int skip, int take, string orderBy);
    }
}
