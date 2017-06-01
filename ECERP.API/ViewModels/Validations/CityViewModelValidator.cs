namespace ECERP.API.ViewModels.Validations
{
    using FluentValidation;

    public class CityViewModelValidator: AbstractValidator<CityViewModel>
    {
        public CityViewModelValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(c => c.Name).Must(x => x.Length <= 100).WithMessage("Length of name must be less than 100.");
        }
    }
}
