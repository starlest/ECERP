namespace ECERP.Data.Abstract
{
    using Models.Entities.FinancialAccounting;

    public interface ILedgerAccountRepository : IRepository<int, LedgerAccount>
    {
    }
}