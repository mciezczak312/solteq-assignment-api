using System.Collections.Generic;

namespace EmployeesManagement.Infrastructure
{
    public static class Constants
    {
        public static readonly Dictionary<string, string> OrderableColumnsNamesWhitelist = new Dictionary<string, string>
        {
            {"firstName", "firstName" },
            {"lastName", "lastName" },
            {"email", "email" },
            {"currentSalary", "CurrentSalary" },
            {"positionsNames", "PositionsNames" }
        };

        public enum OrderDirection
        {
            ASC,
            DESC
        }

    }
}
