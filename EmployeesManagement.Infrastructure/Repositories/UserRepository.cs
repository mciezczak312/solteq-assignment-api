using System.Text;
using Dapper;
using EmployeesManagement.Core.Interfaces;
using EmployeesManagement.Infrastructure.Data;
using EmployeesManagement.Infrastructure.Helpers;

namespace EmployeesManagement.Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase , IUserRepository
    {
        public UserRepository(DbContext ctx) : base(ctx)
        {
        }


        public bool CheckCredentials(string userName, string password)
        {
            var passwordHash = Encoding.ASCII.GetBytes(password).ToMd5Hash();

            using (var conn = _context.GetConnection())
            {
                conn.Open();
                var user = conn.QueryFirstOrDefault(@"
                        select * from User
                        where userName = @userName
                        and passwordHash = @passwordHash", new {userName = userName, passwordHash = passwordHash});
                return user != null;

            }
        }
    }
}
