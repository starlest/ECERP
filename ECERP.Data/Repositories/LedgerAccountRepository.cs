namespace ECERP.Data.Repositories
{
    using Abstract;
    using Models.Entities.FinancialAccounting;

    public class LedgerAccountRepository : Repository<int, LedgerAccount>, ILedgerAccountRepository
    {
        public LedgerAccountRepository(ECERPDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}