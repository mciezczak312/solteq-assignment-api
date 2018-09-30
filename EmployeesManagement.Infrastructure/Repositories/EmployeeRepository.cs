using EmployeesManagement.Core.Entities;
using EmployeesManagement.Core.Interfaces;
using EmployeesManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using EmployeesManagement.Core.DTO;

namespace EmployeesManagement.Infrastructure.Repositories
{
    public class EmployeeRepository : RepositoryBase, IEmployeeRepository
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

        public SearchResponseDto SearchEmployees(string searchTerm, int skip, int take, string orderBy)
        {
            var sql = @"
                    SELECT Employee.id, Employee.firstName, Employee.lastName, Employee.email, S.amount as CurrentSalary, GROUP_CONCAT(PositionName) as PositionsNames FROM Employee
                    join Salary S on Employee.id = S.employee_id
                    left join PositionEmployee E on Employee.id = E.EmployeeID
                    left join Position P on E.PositionId = P.Id
                    where (current_date() between S.fromDate and S.toDate)";

            var sqlTotalCount = @"
                select count(*)
                from (SELECT Employee.id,
                             Employee.firstName,
                             Employee.lastName,
                             Employee.email,
                             S.amount                   as CurrentSalary,
                             GROUP_CONCAT(PositionName) as PositionsNames
                      FROM Employee
                             join Salary S on Employee.id = S.employee_id
                             left join PositionEmployee E on Employee.id = E.EmployeeID
                             left join Position P on E.PositionId = P.Id
                      where (current_date() between S.fromDate and S.toDate)";

            if (searchTerm != null)
            {
                searchTerm = "%"+searchTerm+"%";
                var addedSql = @"and (Employee.firstName like '@SearchTerm' or Employee.lastName like '@SearchTerm' or Employee.email like '@SearchTerm')";
                sql += addedSql;
                sqlTotalCount += addedSql;
                
                sqlTotalCount = sqlTotalCount.Replace("@SearchTerm", searchTerm);
                sql = sql.Replace("@SearchTerm", searchTerm);
            }
            sql += @"
                    group by Employee.id, S.amount
                    order by @OrderBy
                    limit @Skip, @Take;";
            sql = sql.Replace("@OrderBy", orderBy);

            sqlTotalCount += @"group by Employee.id, S.amount) as TotalCount";

            using (var conn = _context.GetConnection())
            {
                conn.Open();
                var result = conn.Query<EmployeesSearchResultDto>(sql, new
                {
                    OrderBy = orderBy,
                    Skip = skip,
                    Take = take
                });

                var totalCount = conn.Query<long>(sqlTotalCount).FirstOrDefault();

                return new SearchResponseDto()
                {
                    SearchResults = result,
                    TotalCount = totalCount
                };
            }

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
