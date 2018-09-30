using EmployeesManagement.Core.Interfaces;
using EmployeesManagement.Infrastructure.Data;
using System.Collections.Generic;
using Dapper;
using EmployeesManagement.Core.Entities;

namespace EmployeesManagement.Infrastructure.Repositories
{
    public class AddressRepository : RepositoryBase, IRepository<Address>
    {
        public AddressRepository(DbContext ctx) : base(ctx)
        {
        }

        public Address GetById(int id)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                return conn.QueryFirstOrDefault<Address>("SELECT * FROM Address WHERE Id=@Id", new { Id = id });
            }
        }

        public void Insert(Address item)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Address> ListAll()
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                return conn.Query<Address>("SELECT * FROM Address");
            }
        }
    }
}
