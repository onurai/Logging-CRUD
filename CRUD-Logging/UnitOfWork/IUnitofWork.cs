using CRUD_Logging.Repository.Interface;

namespace CRUD_Logging.UnitOfWork
{
    public interface IUnitofWork
    {
        public IEmployeeRepository employeeRepository { get; }

        public Task Commit();
    }
}
