namespace ECERP.API.ViewModels.Validations
{
    using FluentValidation;

    public class ProductViewModelValidator : AbstractValidator<ProductViewModel>
    {
        public ProductViewModelValidator()
        {
            RuleFor(p => p.ProductId).NotEmpty().WithMessage("Product identifier cannot be empty.");
            RuleFor(p => p.ProductId).Must(x => x.Length <= 50).WithMessage("Length of product identifier must be less than 50.");
            RuleFor(p => p.Name).NotEmpty().WithMessage("Name cannot be empty.");
            RuleFor(p => p.Name).Must(x => x.Length <= 100).WithMessage("Length of name must be less than 100.");
            RuleFor(p => p.PrimaryUnitName).NotEmpty().WithMessage("Primary unit name cannot be empty.");
            RuleFor(p => p.PrimaryUnitName).Must(x => x.Length <= 10).WithMessage("Length of primary unit name must be less than 10.");
            RuleFor(p => p.SecondaryUnitName).NotEmpty().WithMessage("Secondary unit name cannot be empty.");
            RuleFor(p => p.SecondaryUnitName).Must(x => x.Length <= 10).WithMessage("Length of secondary unit name must be less than 10.");
            RuleFor(p => p.QuantityPerPrimaryUnit).NotNull().WithMessage("Quantity per primary unit cannot be null.");
            RuleFor(p => p.QuantityPerSecondaryUnit).NotNull().WithMessage("Quantity per secondary unit cannot be null.");
            RuleFor(p => p.SalesPrice).NotNull().WithMessage("Sales price cannot be null.");
            RuleFor(p => p.PurchasePrice).NotNull().WithMessage("Purchase price cannot be null.");
            RuleFor(p => p.ProductCategory).NotEmpty().WithMessage("Product category identifier cannot be empty.");
        }
    }
}
