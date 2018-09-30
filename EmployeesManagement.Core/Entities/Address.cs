namespace EmployeesManagement.Core.Entities
{
    public class Address : BaseEntity
    {
        public string Street { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}
