namespace ECERP.Business.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Abstract;
    using Data.Abstract;
    using Models.Entities;
    using Models.Entities.FinancialAccounting;

    public class LedgerAccountService : ILedgerAccountService
    {
        #region Private Fields
        private readonly IRepository _repository;
        #endregion

        #region Constructor
        public LedgerAccountService(IRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Interface Methods
        public IEnumerable<LedgerAccount> GetAll()
        {
            return _repository.GetAll<LedgerAccount>();
        }

        public IEnumerable<LedgerAccount> GetAllByCompany(string company)
        {
            return _repository.Get<LedgerAccount>(la => la.ChartOfAccounts.Company.Name.Equals(company));
        }

        public LedgerAccount GetSingleById(int id)
        {
            return _repository.GetById<LedgerAccount>(id);
        }

        public LedgerAccount GetSingleByName(string name)
        {
            return _repository.GetOne<LedgerAccount>(la => la.Name.Equals(name));
        }

        public int GetNewAccountNumber(int chartOfAccountsId, LedgerAccountGroup group)
        {
            var accountNumber = (int) group * 10000 + 1;
            var lastAccount = _repository
                .Get<LedgerAccount>(
                    la => la.ChartOfAccountsId.Equals(chartOfAccountsId) && la.Group.Equals(group),
                    accounts => accounts.OrderByDescending(account => account.AccountNumber))
                .FirstOrDefault();
            return lastAccount?.AccountNumber + 1 ?? accountNumber;
        }

        public void CreateLedgerAccount(string name, string description, bool isActive, LedgerAccountType type,
            LedgerAccountGroup group, int chartOfAccountsId, ApplicationUser createdBy)
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
            _repository.Create(ledgerAccount, createdBy);
            _repository.Save();
        }
        #endregion
    }
}