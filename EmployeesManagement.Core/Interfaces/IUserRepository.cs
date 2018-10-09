namespace EmployeesManagement.Core.Interfaces
{
    public interface IUserRepository
    {
        bool CheckCredentials(string userName, string password);
    }
}
