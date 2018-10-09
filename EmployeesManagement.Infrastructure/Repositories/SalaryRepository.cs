using EmployeesManagement.Core.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using EmployeesManagement.Core.Entities;
using EmployeesManagement.Infrastructure.Data;
using MySql.Data.MySqlClient;

namespace EmployeesManagement.Infrastructure.Repositories
{
    public class SalaryRepository : RepositoryBase, IRepository<Salary>
    {
        public SalaryRepository(DbContext ctx) : base(ctx)
        {
        }

        public Salary GetById(int id)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                return conn.QueryFirstOrDefault<Salary>("SELECT * FROM Salary WHERE Id=@Id", new { Id = id });
            }
        }

        public int Insert(Salary item, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var conn = connection ?? _context.GetConnection();
            
            var sql = @"INSERT INTO `Salary` (`amount`, `employee_id`, `fromDate`, `toDate`) 
                        VALUES (@Amount, @EmployeeId, @FromDate, @ToDate);
                        SELECT LAST_INSERT_ID()";
            if (transaction != null)
            {
                var res = conn.Query<int>(sql,
                    new
                    {
                        item.Amount,
                        item.EmployeeId,
                        item.FromDate,
                        item.ToDate
                    }, transaction).Single();
                return res;
            }
            else
            {
                conn.Open();
                var res = conn.Query<int>(sql,
                    new
                    {
                        item.Amount,
                        item.EmployeeId,
                        item.FromDate,
                        item.ToDate
                    }).Single();

                conn.Close();
                return res;
            }
                

            
        }

        public int Update(Salary item, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            throw new System.NotImplementedException();
        }

        public Salary GetByEmployeeId(int employeeId)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                return conn.QueryFirstOrDefault<Salary>(@"
                            select amount as Amount, min(fromDate) as FromDate, max(toDate) as ToDate
                                from Salary
                            where employee_id = @Id
                            group by amount
                            order by FromDate desc
                            limit 1;", new { Id = employeeId });
            }
        }



        public IEnumerable<Salary> ListAll()
        {
            var sql = "SELECT * FROM Salary";
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                var result = conn.Query<Salary>(sql);
                return result;
            }
        }
    }
}
