namespace ECERP.Business.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Abstract;
    using Data.Abstract;
    using Models.Entities.FinancialAccounting;

    public class LedgerAccountService : ILedgerAccountService
    {
        #region Private Fields
        private readonly ILedgerAccountRepository _ledgerAccountsRepository;
        #endregion

        #region Constructor
        public LedgerAccountService(ILedgerAccountRepository ledgerAccountsRepository)
        {
            _ledgerAccountsRepository = ledgerAccountsRepository;
        }
        #endregion

        #region Interface Methods
        public IEnumerable<LedgerAccount> GetAll()
        {
            return _ledgerAccountsRepository.GetAll();
        }

        public IEnumerable<LedgerAccount> GetAllByCompany(string company)
        {
            return _ledgerAccountsRepository.FindBy(la => la.ChartOfAccounts.Company.Name.Equals(company));
        }

        public LedgerAccount GetSingleById(int id)
        {
            return _ledgerAccountsRepository.GetSingle(id);
        }

        public LedgerAccount GetSingleByName(string name)
        {
            return _ledgerAccountsRepository.GetSingle(la => la.Name.Equals(name));
        }

        public int GetNewAccountNumber(int chartOfAccountsId, LedgerAccountGroup group)
        {
            var accountNumber = (int) group * 10000 + 1;
            var lastAccount = _ledgerAccountsRepository
                .FindBy(la => la.ChartOfAccountsId.Equals(chartOfAccountsId) && la.Group.Equals(group))
                .OrderByDescending(la => la.AccountNumber)
                .FirstOrDefault();
            return lastAccount?.AccountNumber + 1 ?? accountNumber;
        }

        public void CreateLedgerAccount(string name, string description, bool isActive, LedgerAccountType type,
            LedgerAccountGroup group, int chartOfAccountsId)
        {
            var ledgerAccount = new LedgerAccount
            {
                AccountNumber = GetNewAccountNumber(chartOfAccountsId, group),
                Name = name,
                Description = description,
                IsActive = true,
                IsDefault = false,
                Type = type,
                Group = group,
                ChartOfAccountsId = chartOfAccountsId
            };
            _ledgerAccountsRepository.Add(ledgerAccount);
            _ledgerAccountsRepository.Commit();
        }
        #endregion
    }
}