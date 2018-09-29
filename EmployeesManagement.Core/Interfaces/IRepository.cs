using EmployeesManagement.Core.Entitites;
using System.Collections.Generic;

namespace EmployeesManagement.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> ListAll();
        T GetById(int id);
        void Insert(T item);
    }
}
