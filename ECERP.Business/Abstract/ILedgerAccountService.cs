namespace ECERP.Business.Abstract
{
    using System.Collections.Generic;
    using Models.Entities;
    using Models.Entities.FinancialAccounting;

    public interface ILedgerAccountService
    {
        IEnumerable<LedgerAccount> GetAll();
        IEnumerable<LedgerAccount> GetAllByCompany(string company);
        LedgerAccount GetSingleById(int id);
        LedgerAccount GetSingleByName(string name);
        int GetNewAccountNumber(int chartOfAccountsId, LedgerAccountGroup group);
        void CreateLedgerAccount(string name, string description, bool isActive, LedgerAccountType type,
            LedgerAccountGroup group, int chartOfAccountsId, ApplicationUser createdBy);
    }
}