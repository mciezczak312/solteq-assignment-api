﻿using EmployeesManagement.Core.Interfaces;
using EmployeesManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using EmployeesManagement.Core.Entities;
using MySql.Data.MySqlClient;

namespace EmployeesManagement.Infrastructure.Repositories
{
    public class PositionRepository : RepositoryBase, IRepository<Position>
    {
        public PositionRepository(DbContext ctx) : base(ctx)
        {
        }

        public Position GetById(int id)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                
                return conn.QueryFirstOrDefault<Position>("SELECT * FROM Position WHERE Id=@Id", new { Id = id });
            }
        }

        public int Insert(Position item, IDbConnection conn = null, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public int Update(Position item, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Position> ListAll()
        {
            throw new NotImplementedException();
        }
    }
}
