namespace ECERP.Services.Abstract.FinancialAccounting
{
    using Core.Domain.FinancialAccounting;

    public interface ILedgerTransactionService
    {
        LedgerTransaction GetSingleById(int id);
        void CreateTransaction(LedgerTransaction transaction, int coaId, string createdBy);
    }
}