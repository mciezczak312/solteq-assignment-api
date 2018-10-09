using System.Collections.Generic;

namespace EmployeesManagement.Core.DTO
{
    public class SearchResponseDto
    {
        public IEnumerable<EmployeesSearchResultDto> SearchResults { get; set; }

        public int TotalCount { get; set; }
    }
}
