using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeesManagement.Core.DTO
{
    public class SearchResponseDto
    {
        public IEnumerable<EmployeesSearchResultDto> SearchResults { get; set; }

        public int TotalCount { get; set; }
    }
}
