namespace ECERP.Business.Services
{
    using System.Collections.Generic;
    using Abstract;
    using Data.Abstract;
    using Models.Entities.FinancialAccounting;

    public class ChartOfAccountsService : IChartOfAccountsService
    {
        #region Private Fields
        private readonly IChartOfAccountsRepository _chartOfAccountsRepository;
        #endregion

        #region Constructor
        public ChartOfAccountsService(IChartOfAccountsRepository chartOfAccountsRepository)
        {
            _chartOfAccountsRepository = chartOfAccountsRepository;
        }
        #endregion

        #region Interface Methods
        public IEnumerable<ChartOfAccounts> GetAll()
        {
            return _chartOfAccountsRepository.GetAll(c => c.Company, c => c.LedgerAccounts);
        }

        public ChartOfAccounts GetSingleById(int id)
        {
            return _chartOfAccountsRepository.GetSingle(id, coa => coa.LedgerAccounts, coa => coa.Company);
        }

        public ChartOfAccounts GetSingleByCompanyName(string companyName)
        {
            return _chartOfAccountsRepository.GetSingle(c => c.Company.Name.Equals(companyName), c => c.Company,
                c => c.LedgerAccounts);
        }
        #endregion
    }
}