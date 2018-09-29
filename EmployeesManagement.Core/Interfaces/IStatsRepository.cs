using System.Collections.Generic;

namespace EmployeesManagement.Core.Interfaces
{
    public interface IStatsRepository
    {
        IEnumerable<dynamic> ExecuteSql(string sql);
    }
}
