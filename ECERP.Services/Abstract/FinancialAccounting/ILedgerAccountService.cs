namespace ECERP.Business.Abstract.FinancialAccounting
{
    using System.Collections.Generic;
    using Core.Domain.FinancialAccounting;

    public interface ILedgerAccountService
    {
        IEnumerable<LedgerAccount> GetAll();
        IEnumerable<LedgerAccount> GetAllByCompany(string company);
        LedgerAccount GetSingleById(int id);
        LedgerAccount GetSingleByName(int coaId, string name);
        decimal? GetPeriodBalance(LedgerAccount ledgerAccount, int year, int month);
        decimal GetCurrentBalance(LedgerAccount ledgerAccount);
        int GetNewAccountNumber(int chartOfAccountsId, LedgerAccountGroup group);
        void Create(string name, string description, bool isActive, LedgerAccountType type,
            LedgerAccountGroup group, int chartOfAccountsId, string createdBy);
        bool IsIncrement(LedgerAccountType type, bool isDebit);
    }
}