namespace ECERP.API.ViewModels.Validations
{
    using System;
    using Core.Domain.FinancialAccounting;
    using FluentValidation;

    public class LedgerAccountViewModelValidator : AbstractValidator<LedgerAccountViewModel>
    {
        public LedgerAccountViewModelValidator()
        {
            RuleFor(lavm => lavm.Name).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(lavm => lavm.Name).Must(x => x.Length <= 50).WithMessage("Length of name must be less than 50.");
            RuleFor(lavm => lavm.Description).NotEmpty().WithMessage("Description cannot be empty");
            RuleFor(lavm => lavm.Description).Must(x => x.Length <= 500).WithMessage("Length of description must be less than 500.");
            RuleFor(lavm => lavm.IsHidden).NotNull().WithMessage("IsHidden cannot be null");
            RuleFor(lavm => lavm.Group).NotEmpty().WithMessage("Group cannot be empty");
            RuleFor(lavm => lavm.Group)
                .Must(x => Enum.IsDefined(typeof(LedgerAccountGroup), x))
                .WithMessage("Group is invalid");
            RuleFor(lavm => lavm.ChartOfAccountsId)
                .NotNull()
                .WithMessage("Chart of accounts identifier cannot be null");
        }
    }
}