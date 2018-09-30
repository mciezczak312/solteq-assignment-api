using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeesManagement.Core.DTO
{
    public class SearchResponseDto
    {
        public IEnumerable<EmployeesSearchResultDto> SearchResults { get; set; }

        public long TotalCount { get; set; }
    }
}
