using CRUD_Logging.Data.Entity;
using CRUD_Logging.Dto;
using CRUD_Logging.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CRUD_Logging.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IUnitofWork unitOfWork, ILogger<EmployeeController> logger)
        {
            _unitofWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Request accepted at {0}", DateTime.Now);
            var result = await _unitofWork.employeeRepository.GetAll();
            _logger.LogWarning($"Request Successfully  completed at {DateTime.Now}, and result is {JsonSerializer.Serialize(result)}");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeDto empDto)
        {
            if (ModelState.IsValid != true)
            {
                _logger.LogError("We met error unhandled at", DateTime.Now);
                return BadRequest(ModelState);
            }

            Employee employee = new()
            {
                Name = empDto.Name,
                Surname = empDto.Surname,
                BirthDate = empDto.BirthDate,   
                Position = empDto.Position, 
                Salary = empDto.Salary, 
                IsManager = empDto.IsManager,   
            };
            _logger.LogInformation("Request launched and Employee created at {0}", DateTime.Now);
            await _unitofWork.employeeRepository.Add(employee);
            _logger.LogInformation("Employee added at {0}", DateTime.Now);
            await _unitofWork.Commit();
            _logger.LogInformation("Request finished and Data was saved at {0}", DateTime.Now);
            return Ok(employee);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, EmployeeDto empDto)
        {
            try
            {
                var employee = await _unitofWork.employeeRepository.Find(id);
                _logger.LogInformation($"Employee got from db with Id of {id}");
                employee.Position = empDto.Position;
                employee.IsManager = empDto.IsManager;
                employee.Salary = empDto.Salary;

                _logger.LogInformation("Request launched at {0}", DateTime.Now);
                await _unitofWork.employeeRepository.Update(employee);
                _logger.LogDebug($"Employee updated from db with Id of {id}");
                await _unitofWork.Commit();
                _logger.LogInformation($"Request is completed successfully, Employee with ID of {id} and name of {employee.Name}");
                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured when deleting the employee i-th id of {id}");
                throw ex;
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                var result = await _unitofWork.employeeRepository.Find(id);
                _logger.LogInformation($"Employee got from db with Id of {id}");
                await _unitofWork.employeeRepository.Delete(result);
                _logger.LogDebug($"Employee deleted from db with Id of {id}");
                await _unitofWork.Commit();
                _logger.LogInformation($"Request is completed successfully, Employee with ID of {id} and name of {result.Name}");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured when deleting the employee i-th id of {id}");
                throw ex;
            }
        }

        [HttpGet("Id")]
        public async Task<IActionResult> GetId(int id)
        {
            var result = await _unitofWork.employeeRepository.Find(id);
            _logger.LogInformation("Employee searched was found at {0}", DateTime.Now);
            if (result == null)
            {
                _logger.LogInformation("Employee was not found at {0}", DateTime.Now);
                return NotFound();
            }
            _logger.LogInformation("Employee was successfully found at {0}", DateTime.Now);
            return Ok(result);
        }
    }
}
