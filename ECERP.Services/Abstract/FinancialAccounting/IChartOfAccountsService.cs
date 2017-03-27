namespace ECERP.Business.Abstract.FinancialAccounting
{
    using System.Collections.Generic;
    using Core.Domain.FinancialAccounting;

    public interface IChartOfAccountsService
    {
        IEnumerable<ChartOfAccounts> GetAll();
        ChartOfAccounts GetSingleById(int id);
        ChartOfAccounts GetSingleByCompanyName(string name);
    }
}