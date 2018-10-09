using EmployeesManagement.Core.Interfaces;
using EmployeesManagement.Infrastructure.Data;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using EmployeesManagement.Core.Entities;
using MySql.Data.MySqlClient;

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

        public int Insert(Address item, IDbConnection conn = null, IDbTransaction transaction = null)
        {
            var connection = conn ?? _context.GetConnection();

            var sql = @"INSERT INTO `Address` (`street`, `city`, `zip`, `country`) 
                            VALUES (@Street, @City, @Zip, @Country);
                            SELECT LAST_INSERT_ID()";

            if (transaction != null)
                return connection.Query<int>(sql,
                    new
                    {
                        item.Street,
                        item.City,
                        item.Zip,
                        item.Country
                    }, transaction).Single();

            var res = connection.Query<int>(sql,
                new
                {
                    item.Street,
                    item.City,
                    item.Zip,
                    item.Country
                }).Single();
            connection.Close();
            return res;

        }

        public int Update(Address item, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var conn = connection ?? _context.GetConnection();
            var sql = @"UPDATE Address
                        SET Address.city    = @City,
                            Address.street  = @Street,
                            Address.country = @Country,
                            Address.zip     = @Zip
                        WHERE Address.id = @Id;";

            if (transaction != null)
                return connection.Execute(sql,
                    new
                    {
                        item.Street,
                        item.City,
                        item.Zip,
                        item.Country,
                        item.Id
                    }, transaction);

            var res = connection.Execute(sql,
                new
                {
                    item.Street,
                    item.City,
                    item.Zip,
                    item.Country
                });
            conn.Close();
            return res;


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
