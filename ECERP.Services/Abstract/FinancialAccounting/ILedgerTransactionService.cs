namespace ECERP.Business.Abstract.FinancialAccounting
{
    using Models.Entities.FinancialAccounting;

    public interface ILedgerTransactionService
    {
        LedgerTransaction GetSingleById(int id);
        void CreateTransaction(LedgerTransaction transaction, int coaId, string createdBy);
    }
}