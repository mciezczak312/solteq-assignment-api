using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeesManagement.Core.Interfaces
{
    public interface IUserRepository
    {
        bool CheckCredentials(string userName, string password);
    }
}
