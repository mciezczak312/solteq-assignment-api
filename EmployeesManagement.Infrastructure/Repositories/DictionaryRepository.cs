using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using EmployeesManagement.Core.Entities;
using EmployeesManagement.Core.Interfaces;
using EmployeesManagement.Infrastructure.Data;

namespace EmployeesManagement.Infrastructure.Repositories
{
    public class DictionaryRepository: RepositoryBase, IDictionaryRepository
    {
        public DictionaryRepository(DbContext ctx) : base(ctx)
        {
        }

        public IEnumerable<Position> GetPositionsDictionary()
        {
            var sql = "SELECT * FROM Position";
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                var result = conn.Query<Position>(sql);
                return result;
            }
        }
    }
}
