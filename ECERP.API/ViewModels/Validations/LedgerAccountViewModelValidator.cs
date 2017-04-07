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
            RuleFor(lavm => lavm.Description).NotEmpty().WithMessage("Description cannot be empty");
            RuleFor(lavm => lavm.IsHidden).NotNull().WithMessage("IsHidden cannot be null");
            RuleFor(lavm => lavm.Type).NotEmpty().WithMessage("Type cannot be empty");
            RuleFor(lavm => lavm.Type)
                .Must(x => Enum.IsDefined(typeof(LedgerAccountType), x))
                .WithMessage("Type is invalid");
            RuleFor(lavm => lavm.Group).NotEmpty().WithMessage("Group cannot be empty");
            RuleFor(lavm => lavm.Group)
                .Must(x => Enum.IsDefined(typeof(LedgerAccountGroup), x))
                .WithMessage("Group is invalid");
            RuleFor(lavm => lavm.ChartOfAccountsId)
                .NotEmpty()
                .WithMessage("Chart of accounts identifier cannot be empty");
        }
    }
}