using CRUD_Logging.Dto;
using FluentValidation;

namespace CRUD_Logging.Validation
{
    public class EmployeeValidator : AbstractValidator<EmployeeDto>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.Name).MaximumLength(20);
            RuleFor(x => x.Surname).MaximumLength(20);
            RuleFor(x => x.Position).MaximumLength(20);
            RuleFor(x => x.IsManager);
            RuleFor(x => x.Salary).GreaterThanOrEqualTo(350);
            RuleFor(x => x.BirthDate);

            //RuleFor(x => x.BirthDate).Must(BeAValidAge).WithMessage("BirthtDate is InValid");
        }

        //protected bool BeAValidAge(DateTime date)
        //{
        //    int currentYear = DateTime.Now.Year;
        //    int dobYear = date.Year;

        //    if ((currentYear - 24) == dobYear)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
    }
}
