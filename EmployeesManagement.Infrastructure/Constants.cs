using System.Collections.Generic;
using System.Collections.Immutable;

namespace EmployeesManagement.Infrastructure
{
    public static class Constants
    {
        public static readonly ImmutableDictionary<string,string> OrderableColumnsNamesWhitelist = new Dictionary<string, string>
        {
            {"firstName", "firstName" },
            {"lastName", "lastName" },
            {"email", "email" },
            {"currentSalary", "CurrentSalary" },
            {"positionsNames", "PositionsNames" }
        }.ToImmutableDictionary();

        public enum OrderDirection
        {
            ASC,
            DESC
        }

    }
}
