namespace EmployeesManagement.API.Interfaces
{
    public interface IUserService
    {
        string Authenticate(string userName, string password);
    }
}