using CRUD_Logging.Data.Context;
using CRUD_Logging.Data.Entity;
using CRUD_Logging.Dto;
using CRUD_Logging.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Logging.Repository.Implementation
{
    public class EmployeeRepository : EfRepository<Employee, int>, IEmployeeRepository
    {
        private readonly AppDbContext _appDbContext;

        public EmployeeRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;  
        }

        public async Task<Employee> Create(EmployeeDto empDto)
        {
            Employee employee = new()
            {
                Name = empDto.Name,
                Surname = empDto.Surname,
                Salary = empDto.Salary,
                Position = empDto.Position,
                IsManager = empDto.IsManager,
                BirthDate = empDto.BirthDate,   
            };
            _appDbContext.Employees.Add(employee);
            await _appDbContext.SaveChangesAsync();
            return employee;
        }

        public async Task<List<Employee>> GetAll()
        {
            List<Employee> employees = await _appDbContext.Employees.ToListAsync();
            return employees;
        }

        public Task<Employee> Get(int id)
        {
            var result = _appDbContext.Employees.FirstOrDefaultAsync(i => i.Id == id);
            return result;
        }

        public async Task<bool> Remove(int id)
        {
            var result = await _appDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            _appDbContext.Employees.Remove(result);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(int id, EmployeeDto empDto)
        {
            Employee employee = await _appDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
  
            employee.IsManager = empDto.IsManager;
            employee.Salary = empDto.Salary;
            employee.Position = empDto.Position;
            
            await _appDbContext.SaveChangesAsync();
            return true;
        }
    }
}
