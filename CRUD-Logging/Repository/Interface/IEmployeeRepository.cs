using CRUD_Logging.Data.Entity;
using CRUD_Logging.Dto;
using Microsoft.Extensions.Hosting;

namespace CRUD_Logging.Repository.Interface
{
    public interface IEmployeeRepository : IRepository<Employee, int>
    {
        Task<List<Employee>> GetAll();
        Task<Employee> Get(int id);
        Task<Employee> Create(EmployeeDto empDto);
        Task<bool> Update(int id, EmployeeDto empDto);
        Task<bool> Remove(int id);
    }
}
