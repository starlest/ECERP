namespace ECERP.API.ViewModels.Validations
{
    using System.Linq;
    using FluentValidation;

    public class LedgerTransactionViewModelValidator : AbstractValidator<LedgerTransactionViewModel>
    {
        public LedgerTransactionViewModelValidator()
        {
            RuleFor(ltvm => ltvm.Description).NotEmpty().WithMessage("Description cannot be empty.");
            RuleFor(ltvm => ltvm.Description)
                .Must(x => x.Length <= 50)
                .WithMessage("Length of description must be less than 500.");

            RuleFor(ltvm => ltvm.Documentation).NotEmpty().WithMessage("Documentation cannot be empty.");
            RuleFor(ltvm => ltvm.Documentation)
                .Must(x => x.Length <= 500)
                .WithMessage("Length of documentation must be less than 50.");

            RuleFor(ltvm => ltvm.PostingDate).NotEmpty().WithMessage("Posting date cannot be empty.");

            RuleFor(ltvm => ltvm.IsEditable).NotNull().WithMessage("IsEditable cannot be null.");

            RuleFor(ltvm => ltvm.ChartOfAccountsId)
                .NotNull()
                .WithMessage("Chart of accounts identifier cannot be null.");

            RuleFor(ltvm => ltvm.LedgerTransactionLines)
                .NotNull()
                .WithMessage("Ledger transaction lines cannot be null.");
        }
    }
}