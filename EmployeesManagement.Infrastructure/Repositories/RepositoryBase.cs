using EmployeesManagement.Infrastructure.Data;

namespace EmployeesManagement.Infrastructure.Repositories
{
    public class RepositoryBase
    {
        protected readonly DbContext _context;

        public RepositoryBase(DbContext ctx)
        {
            this._context = ctx;
        }
    }
}
