using System.Collections.Generic;
using EmployeesManagement.API.Interfaces;
using EmployeesManagement.Core.Entities;
using EmployeesManagement.Core.Interfaces;

namespace EmployeesManagement.API.Services
{
    public class DictionaryService : IDictionaryService
    {
        private readonly IDictionaryRepository _repository;

        public DictionaryService(IDictionaryRepository repo)
        {
            _repository = repo;
        }

        public IEnumerable<Position> GetPositionsDictionary()
        {
            return _repository.GetPositionsDictionary();
        }
    }
}
