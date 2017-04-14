namespace ECERP.API.ViewModels.Validations
{
    using System.Linq;
    using FluentValidation;

    public class LedgerTransactionViewModelValidator : AbstractValidator<LedgerTransactionViewModel>
    {
        public LedgerTransactionViewModelValidator()
        {
            RuleFor(ltvm => ltvm.Description).NotEmpty().WithMessage("Description cannot be empty");
            RuleFor(ltvm => ltvm.Description)
                .Must(x => x.Length <= 50)
                .WithMessage("Length of description must be less than 500.");

            RuleFor(ltvm => ltvm.Documentation).NotEmpty().WithMessage("Documentation cannot be empty");
            RuleFor(ltvm => ltvm.Documentation)
                .Must(x => x.Length <= 500)
                .WithMessage("Length of documentation must be less than 50.");

            RuleFor(ltvm => ltvm.PostingDate).NotEmpty().WithMessage("Posting date cannot be empty");

            RuleFor(ltvm => ltvm.ChartOfAccountsId)
                .NotNull()
                .WithMessage("Chart of accounts identifier cannot be null");

            RuleFor(ltvm => ltvm.LedgerTransactionLines)
                .NotNull()
                .WithMessage("Ledger transaction lines cannot be null");
//            RuleFor(ltvm => ltvm.LedgerTransactionLines)
//                .Must(lines =>
//                {
//                    var totalDebit = 0m;
//                    var totalCredit = 0m;
//                    foreach (var line in lines)
//                    {
//                        if (line.IsDebit) totalDebit += line.Amount;
//                        else totalCredit += line.Amount;
//                    }
//                    return totalDebit == totalCredit;
//                })
//                .WithMessage("Ledger transaction must have equal total debit and total credit amounts.");
//            RuleFor(ltvm => ltvm.LedgerTransactionLines)
//                .Must(lines =>
//                {
//                    return
//                        lines.Any(
//                            line => lines.Count(l => l.LedgerAccountId.Equals(line.LedgerAccountId)) > 1);
//                })
//                .WithMessage("Ledger transaction lines cannot contain duplicate ledger accounts.");
        }
    }
}