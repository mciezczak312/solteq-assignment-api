namespace EmployeesManagement.API.Helpers
{
    public class SearchUrlQuery
    {
        public string Q { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public string OrderBy { get; set; }
    }
}
