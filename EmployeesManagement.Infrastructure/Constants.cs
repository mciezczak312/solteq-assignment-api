using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeesManagement.Infrastructure
{
    public static class Constants
    {
        public static Dictionary<string, string> OrderableColumnsNamesWhitelist = new Dictionary<string, string>()
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
