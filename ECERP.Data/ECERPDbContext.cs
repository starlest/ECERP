namespace ECERP.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Models.Entities;
    using Models.Entities.Companies;
    using Models.Entities.FinancialAccounting;

    public class ECERPDbContext : IdentityDbContext<ApplicationUser>
    {
        #region Constructor
        public ECERPDbContext(DbContextOptions options) : base(options)
        {
        }
        #endregion

        #region Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<ApplicationUser>().HasOne(u => u.CreatedBy).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ApplicationUser>().HasOne(u => u.ModifiedBy).WithMany().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ChartOfAccounts>().ToTable("ChartsOfAccounts");
            modelBuilder.Entity<ChartOfAccounts>().HasOne(coa => coa.Company).WithOne(c => c.ChartOfAccounts);
            modelBuilder.Entity<LedgerAccount>().HasIndex(la => la.AccountNumber).IsUnique();
            modelBuilder.Entity<LedgerAccount>().HasIndex(la => la.Name).IsUnique();
        }
        #endregion

        #region Properties
        public DbSet<Company> Companies { get; set; }
        public DbSet<ChartOfAccounts> ChartsOfAccounts { get; set; }
        public DbSet<LedgerAccount> LedgerAccounts { get; set; }
        #endregion
    }
}