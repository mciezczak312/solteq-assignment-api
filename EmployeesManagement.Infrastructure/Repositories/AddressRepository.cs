using EmployeesManagement.Core.Interfaces;
using EmployeesManagement.Infrastructure.Data;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        public int Insert(Address item)
        {
            var sql = @"INSERT INTO `Address` (`street`, `city`, `zip`, `country`) 
                            VALUES (@Street, @City, @Zip, @Country);
                            SELECT LAST_INSERT_ID()";
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                return conn.Query<int>(sql,
                    new
                    {
                        item.Street,
                        item.City,
                        item.Zip,
                        item.Country
                    }).Single();
            }

        }

        public int Insert(Address item, IDbConnection conn, IDbTransaction transaction)
        {

            var sql = @"INSERT INTO `Address` (`street`, `city`, `zip`, `country`) 
                            VALUES (@Street, @City, @Zip, @Country);
                            SELECT LAST_INSERT_ID()";


            return conn.Query<int>(sql,
                new
                {
                    item.Street,
                    item.City,
                    item.Zip,
                    item.Country
                }, transaction).Single();
        }

        public int Update(Address item)
        {
            var sql = @"UPDATE Address
                        SET Address.city    = @City,
                            Address.street  = @Street,
                            Address.country = @Country,
                            Address.zip     = @Zip
                        WHERE Address.id = @Id;";

            using (var conn = _context.GetConnection())
            {
                conn.Open();
                return conn.Execute(sql,
                    new
                    {
                        item.Street,
                        item.City,
                        item.Zip,
                        item.Country,
                        item.Id
                    });
            }

        }

        public int Update(Address item, IDbConnection connection, IDbTransaction transaction)
        {
            var sql = @"UPDATE Address
                        SET Address.city    = @City,
                            Address.street  = @Street,
                            Address.country = @Country,
                            Address.zip     = @Zip
                        WHERE Address.id = @Id;";


            return connection.Execute(sql,
                new
                {
                    item.Street,
                    item.City,
                    item.Zip,
                    item.Country,
                    item.Id
                }, transaction);
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
