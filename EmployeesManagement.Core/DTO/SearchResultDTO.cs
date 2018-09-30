namespace EmployeesManagement.Core.DTO
{
    public class SearchResultDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public double CurrentSalary { get; set; }
        public string PositionsNames { get; set; }
    }
}
