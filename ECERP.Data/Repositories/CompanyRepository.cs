namespace ECERP.Data.Repositories
{
    using Abstract;
    using Models.Entities.Companies;

    public class CompanyRepository : Repository<int, Company>, ICompanyRepository
    {
        public CompanyRepository(ECERPDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}