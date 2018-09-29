using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using EmployeesManagement.Core.Interfaces;
using EmployeesManagement.Infrastructure.Data;

namespace EmployeesManagement.Infrastructure.Repositories
{
    public class StatsRepository : RepositoryBase, IStatsRepository
    {

        public StatsRepository(DbContext ctx) : base(ctx)
        {
        }


        public IEnumerable<dynamic> ExecuteSql(string sql)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                return conn.Query(sql);
            }
        }
    }
}
