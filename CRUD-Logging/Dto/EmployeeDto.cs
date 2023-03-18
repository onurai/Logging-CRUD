using FluentValidation;

namespace CRUD_Logging.Dto
{
    public class EmployeeDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Position { get; set; }
        public int Salary { get; set; }
        public bool IsManager { get; set; }
    }
}
