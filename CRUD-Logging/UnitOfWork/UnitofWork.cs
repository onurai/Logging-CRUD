using CRUD_Logging.Data.Context;
using CRUD_Logging.Repository.Implementation;
using CRUD_Logging.Repository.Interface;

namespace CRUD_Logging.UnitOfWork
{
    public class UnitofWork : IUnitofWork
    {
        private readonly AppDbContext _appDbContext;
        public IEmployeeRepository employeeRepository { get; }

        public UnitofWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            employeeRepository = new EmployeeRepository(_appDbContext);
        }

        public async Task Commit()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
