namespace ECERP.API.ViewModels.Validations
{
    using FluentValidation;

    public class SupplierViewModelValidator : AbstractValidator<SupplierViewModel>
    {
        public SupplierViewModelValidator()
        {
            RuleFor(supplier => supplier.Name).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(supplier => supplier.Name).Must(x => x.Length <= 50).WithMessage("Length of name must be less than 100.");
            RuleFor(supplier => supplier.Address).NotEmpty().WithMessage("Address cannot be empty");
            RuleFor(supplier => supplier.Address).Must(x => x.Length <= 500).WithMessage("Length of address must be less than 500.");
            RuleFor(supplier => supplier.ContactNumber).NotEmpty().WithMessage("Contact number cannot be empty");
            RuleFor(supplier => supplier.ContactNumber).Must(x => x.Length <= 50).WithMessage("Length of contact number must be less than 50.");
            RuleFor(supplier => supplier.City).NotNull().WithMessage("City cannot be null.");
        }
    }
}
