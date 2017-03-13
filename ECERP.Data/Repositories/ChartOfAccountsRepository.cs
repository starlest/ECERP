namespace ECERP.Data.Repositories
{
    using Abstract;
    using Models.Entities.FinancialAccounting;

    public class ChartOfAccountsRepository : Repository<int, ChartOfAccounts>, IChartOfAccountsRepository
    {
        public ChartOfAccountsRepository(ECERPDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}