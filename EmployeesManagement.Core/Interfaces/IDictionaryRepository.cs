using System.Collections.Generic;
using EmployeesManagement.Core.Entities;

namespace EmployeesManagement.Core.Interfaces
{
    public interface IDictionaryRepository
    {
        IEnumerable<Position> GetPositionsDictionary();

    }
}
