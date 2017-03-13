namespace ECERP.Business.Tests
{
    using System;
    using Abstract;

    public interface IServiceFixture : IDisposable
    {
        IChartOfAccountsService ChartOfAccountsService { get; }
        ICompanyService CompanyService { get; }
        ILedgerAccountService LedgerAccountService { get; }
    }
}