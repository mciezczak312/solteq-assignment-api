﻿using System.Collections.Generic;
using EmployeesManagement.Core.Entities;

namespace EmployeesManagement.API.Interfaces
{
    public interface IDictionaryService
    {
        IEnumerable<Position> GetPositionsDictionary();
    }
}
