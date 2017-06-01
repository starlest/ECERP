namespace ECERP.API.ViewModels.Validations
{
    using FluentValidation;

    public class ProductCategoryViewModelValidator: AbstractValidator<ProductCategoryViewModel>
    {
        public ProductCategoryViewModelValidator()
        {
            RuleFor(pc => pc.Name).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(pc => pc.Name).Must(x => x.Length <= 50).WithMessage("Length of name must be less than 50.");
        }
    }
}
