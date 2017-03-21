namespace ECERP.Business.Services.FinancialAccounting
{
    using System.Collections.Generic;
    using Abstract.FinancialAccounting;
    using Data.Abstract;
    using Models.Entities.FinancialAccounting;

    public class ChartOfAccountsService : IChartOfAccountsService
    {
        #region Private Fields
        private readonly IRepository _repository;
        #endregion

        #region Constructor
        public ChartOfAccountsService(IRepository repository, ILedgerAccountService accountService)
        {
            _repository = repository;
        }
        #endregion

        #region Interface Methods
        public IEnumerable<ChartOfAccounts> GetAll()
        {
            return _repository.GetAll<ChartOfAccounts>(null, null, null, c => c.Company,
                c => c.LedgerAccounts);
        }

        public ChartOfAccounts GetSingleById(int id)
        {
            return _repository.GetById<ChartOfAccounts>(id);
        }

        public ChartOfAccounts GetSingleByCompanyName(string companyName)
        {
            return _repository.GetOne<ChartOfAccounts>(c => c.Company.Name.Equals(companyName),
                c => c.Company, c => c.LedgerAccounts);
        }
        #endregion
    }
}