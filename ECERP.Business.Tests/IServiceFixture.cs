namespace ECERP.Business.Tests
{
    using System;
    using Abstract;
    using Abstract.FinancialAccounting;

    public interface IServiceFixture : IDisposable
    {
        IChartOfAccountsService ChartOfAccountsService { get; }
        ICompanyService CompanyService { get; }
        ILedgerAccountService LedgerAccountService { get; }
        ILedgerTransactionService LedgerTransactionService { get; }
    }
}