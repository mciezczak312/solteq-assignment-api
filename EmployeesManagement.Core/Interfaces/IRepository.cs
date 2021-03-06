﻿using EmployeesManagement.Core.Entities;
using System.Collections.Generic;
using System.Data;

namespace EmployeesManagement.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> ListAll();
        T GetById(int id);
        int Insert(T item);
        int Insert(T item, IDbConnection connection, IDbTransaction transaction);
        int Update(T item);
        int Update(T item, IDbConnection connection, IDbTransaction transaction);
    }
}
