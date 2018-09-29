using EmployeesManagement.Core.Entitites;
using EmployeesManagement.Core.Interfaces;
using System.Collections.Generic;
using Dapper;
using EmployeesManagement.Infrastructure.Data;

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

        public void Insert(Salary item)
        {
            using (var conn = _context.GetConnection())
            {
                var sql = @"INSERT INTO `Salary` (`amount`, `employee_id`, `fromDate`, `toDate`) 
                            VALUES (@Amount, @EmployeeId, @FromDate, @ToDate);";

                conn.Open();
                conn.Execute(sql,
                    new
                    {
                        item.Amount,
                        item.EmployeeId,
                        item.FromDate,
                        item.ToDate
                    });

            }
        }

        public Salary GetByEmployeeId(int employeeId)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                return conn.QueryFirstOrDefault<Salary>("SELECT * FROM Salary WHERE employee_id=@Id", new { Id = employeeId });
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
