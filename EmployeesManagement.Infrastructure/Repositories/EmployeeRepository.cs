using EmployeesManagement.Core.Entitites;
using EmployeesManagement.Core.Interfaces;
using EmployeesManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;

namespace EmployeesManagement.Infrastructure.Repositories
{
    public class EmployeeRepository : RepositoryBase, IRepository<Employee>
    {
        public EmployeeRepository(DbContext ctx) : base(ctx)
        {
        }

        public Employee GetById(int id)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                var result = conn.QueryFirstOrDefault<Employee>("SELECT * FROM Employee WHERE Id=@Id", new { Id = id });
                result.PositionsNames = conn.Query<string>(@"
                            select P.PositionName from Employee e
                            join PositionEmployee E2 on e.id = E2.EmployeeID
                            join Position P on E2.PositionId = P.Id
                            where e.id = @Id;", new { Id = id }).AsList<string>();
                return result;
            }
            
        }

        public void Insert(Employee item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> ListAll()
        {
            var sql = "SELECT * FROM Employee";
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                var result = conn.Query<Employee>(sql);
                return result;
            }
        }
    }
}
