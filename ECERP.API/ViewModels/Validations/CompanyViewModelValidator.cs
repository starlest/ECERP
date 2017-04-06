namespace ECERP.API.ViewModels.Validations
{
    using FluentValidation;
    using ViewModels;

    public class CompanyViewModelValidator : AbstractValidator<CompanyViewModel>
    {
        public CompanyViewModelValidator()
        {
            RuleFor(company => company.Name).NotEmpty().WithMessage("Name cannot be empty");
        }
    }
}