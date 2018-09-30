using System;
using System.Collections.Generic;
using System.Text;
using EmployeesManagement.Core.DTO;

namespace EmployeesManagement.Core.Interfaces
{
    public interface IEmployeeSearchcs
    {
        IEnumerable<SearchResultDto> SearchEmployees(string searchTerm, int page, int take);
    }
}
