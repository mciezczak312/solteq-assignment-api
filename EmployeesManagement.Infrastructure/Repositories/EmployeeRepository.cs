using EmployeesManagement.Core.Entities;
using EmployeesManagement.Core.Interfaces;
using EmployeesManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using Dapper;
using EmployeesManagement.Core.DTO;
using EmployeesManagement.Infrastructure.Helpers;

namespace EmployeesManagement.Infrastructure.Repositories
{
    public class EmployeeRepository : RepositoryBase, IEmployeeRepository
    {
        private readonly IRepository<Address> _addressRepository;
        private readonly SalaryRepository _salaryRepository;

        public EmployeeRepository(
            DbContext ctx,
            IRepository<Address> addressRepository,
            SalaryRepository salaryRepository) : base(ctx)
        {
            _addressRepository = addressRepository;
            _salaryRepository = salaryRepository;
        }

        public Employee GetById(int id)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                var result = conn.QueryFirstOrDefault<Employee>("SELECT * FROM Employee WHERE Id=@Id", new { Id = id });
                var positionsNamesIds = conn.Query<int>(@"
                                SELECT P.Id 
                                FROM Employee e
                                    JOIN PositionEmployee E2 on e.id = E2.EmployeeID
                                    JOIN Position P on E2.PositionId = P.Id
                                WHERE e.id = @Id;", new { Id = id });
                result.PositionsNamesIds = positionsNamesIds.AsList();
                return result;
            }

        }

        public SearchResponseDto SearchEmployees(string searchTerm, int skip, int take, string orderBy)
        {
            ValidateOrderByPattern(orderBy);

            var sql = @"
                    SELECT 
                        Employee.id, 
                        Employee.firstName, 
                        Employee.lastName, 
                        Employee.email, 
                        (Select S1.amount
                         from Salary S1
                         where S1.employee_id = S.employee_id
                           and (current_date() between S1.fromDate and S1.toDate)) as CurrentSalary,
                        GROUP_CONCAT(distinct PositionName) as PositionsNames 
                    FROM Employee
                        JOIN Salary S on Employee.id = S.employee_id
                        LEFT JOIN PositionEmployee E on Employee.id = E.EmployeeID
                        LEFT JOIN Position P on E.PositionId = P.Id
                    ";

            var sqlTotalCount = $@" SELECT COUNT(*) FROM ({sql}";

            if (searchTerm != null)
            {
                var addedSql = @"WHERE (Employee.firstName like CONCAT('%', @SearchTerm, '%') or 
                                 Employee.lastName like CONCAT('%', @SearchTerm, '%')       or 
                                 Employee.email like CONCAT('%', @SearchTerm, '%'))";
                sql += addedSql;
                sqlTotalCount += addedSql;

            }
            sql += @"group by Employee.id, CurrentSalary
                    order by @OrderBy
                    limit @Skip, @Take;";
            sqlTotalCount += @"group by Employee.id, CurrentSalary) as TotalCount";

            var orderSplit = orderBy.Split(";");
            var orderColumnName = GetOrderColumnNameFromWhitelist(orderSplit[0]);
            sql = ReplaceOrderByParameter(sql, orderColumnName, orderSplit[1]);

            using (var conn = _context.GetConnection())
            {
                conn.Open();
                var result = conn.Query<EmployeesSearchResultDto>(sql, new
                {
                    Skip = skip,
                    Take = take,
                    SearchTerm = searchTerm
                });

                var totalCount = conn.Query<int>(sqlTotalCount, new { SearchTerm = searchTerm }).FirstOrDefault();

                return new SearchResponseDto
                {
                    SearchResults = result,
                    TotalCount = totalCount
                };
            }

        }

        public void Delete(int id)
        {
            var sql = "DELETE FROM Employee WHERE id = @Id";

            using (var conn = _context.GetConnection())
            {
                conn.Open();
                var affectedRows = conn.Execute(sql, new { Id = id });
                if (affectedRows == 0)
                {
                    throw new EmployeeNotFoundException($"Could not find employee with id: {id}");
                }
            }
        }

        public int Insert(Employee employee, Address address, IEnumerable<Salary> salaryList)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        var addressId = _addressRepository.Insert(address, conn, transaction);
                        var employeeId = InsertEmployee(employee, addressId, conn, transaction);
                        InsertPositionEmployee(employeeId, employee.PositionsNamesIds, conn, transaction);
                        InsertSalaryList(employeeId, salaryList, conn, transaction);

                        transaction.Commit();
                        return employeeId;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }


                }
            }
        }

        public void Update(Employee employee, Address address, IEnumerable<Salary> salaryList)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        _addressRepository.Update(address, conn, transaction);
                        UpdateEmployee(employee, conn, transaction);
                        UpdatePositionEmployee(employee.Id, employee.PositionsNamesIds, conn, transaction);
                        InsertSalaryList(employee.Id, salaryList, conn, transaction);

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }


                }
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

        #region SearchHelpers 

        private static void ValidateOrderByPattern(string orderBy)
        {
            var regex = new Regex(@"^(?:\w+;)(?:DESC|ASC|desc|asc)$");
            var match = regex.Match(orderBy);
            if (!match.Success)
            {
                throw new ArgumentException("Wrong order by pattern");
            }
        }


        private static string GetOrderColumnNameFromWhitelist(string orderColumnName)
        {
            string result;
            if (Constants.OrderableColumnsNamesWhitelist.ContainsKey(orderColumnName))
            {
                result = Constants.OrderableColumnsNamesWhitelist[orderColumnName];
            }
            else
            {
                throw new IllegalColumnNameException($"You can't sort by column {orderColumnName}");
            }

            return result;
        }

        private static string ReplaceOrderByParameter(string sql, string orderColumnName, string orderDirectionStr)
        {
            Enum.TryParse(orderDirectionStr.ToUpper(), out Constants.OrderDirection orderDirection);
            switch (orderDirection)
            {
                case Constants.OrderDirection.ASC:
                    sql = sql.Replace("@OrderBy", orderColumnName + " ASC");
                    break;
                case Constants.OrderDirection.DESC:
                    sql = sql.Replace("@OrderBy", orderColumnName + " DESC");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return sql;
        }

        #endregion

        private int InsertEmployee(Employee item, int addressId, IDbConnection conn, IDbTransaction transaction)
        {
            var sql = @"INSERT INTO `Employee` (`firstName`, `lastName`, `email`, `gender`, `addressId`) 
                            VALUES (@FirstName, @LastName, @Email, @Gender, @AddressId);
                            SELECT LAST_INSERT_ID()";

            return conn.Query<int>(sql,
                new
                {
                    item.FirstName,
                    item.LastName,
                    item.Email,
                    item.Gender,
                    addressId
                }, transaction).Single();
        }

        private void UpdateEmployee(Employee item, IDbConnection conn, IDbTransaction transaction)
        {
            var sql = @"UPDATE Employee
                        SET Employee.firstName = @FirstName,
                            Employee.lastName = @LastName,
                            Employee.email = @Email,
                            Employee.gender = @Gender
                        WHERE Employee.Id = @Id;";
            conn.Execute(sql, new
            {
                item.FirstName,
                item.LastName,
                item.Email,
                item.Gender,
                item.Id
            }, transaction);
        }

        private void InsertPositionEmployee(int employeeId, IEnumerable<int> positionsIds, IDbConnection conn, IDbTransaction transaction)
        {
            var sql = @"INSERT INTO `PositionEmployee` (`PositionId`, `EmployeeID`) 
                            VALUES (@PositionId, @EmployeeID);";
            foreach (var positionsId in positionsIds)
            {
                conn.QueryAsync(sql, new
                {
                    PositionId = positionsId,
                    EmployeeID = employeeId
                }, transaction);
            }
        }

        private void UpdatePositionEmployee(int employeeId, IEnumerable<int> positionsIds, IDbConnection conn,
            IDbTransaction transaction)
        {
            var sqlDelete = @"DELETE FROM `PositionEmployee` WHERE EmployeeID = @Id";
            conn.Execute(sqlDelete, new {Id = employeeId}, transaction);
            InsertPositionEmployee(employeeId, positionsIds, conn, transaction);
        }

        private void InsertSalaryList(int employeeId, IEnumerable<Salary> salaryList, IDbConnection conn, IDbTransaction transaction)
        {
            foreach (var salary in salaryList)
            {
                salary.EmployeeId = employeeId;
                _salaryRepository.Insert(salary, conn, transaction);
            }
        }

    }
}
