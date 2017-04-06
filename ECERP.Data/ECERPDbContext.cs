namespace ECERP.Data
{
    using Core.Domain;
    using Core.Domain.Companies;
    using Core.Domain.Configuration;
    using Core.Domain.Customers;
    using Core.Domain.FinancialAccounting;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Models.Entities;

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

            modelBuilder.Entity<CompanySetting>().HasIndex(p => p.Key).IsUnique();

            modelBuilder.Entity<Company>().HasIndex(c => c.Name).IsUnique();

            modelBuilder.Entity<ApplicationUser>().ToTable("Users");

            modelBuilder.Entity<ChartOfAccounts>().ToTable("ChartsOfAccounts");
            modelBuilder.Entity<ChartOfAccounts>().HasOne(coa => coa.Company).WithOne(c => c.ChartOfAccounts);

            modelBuilder.Entity<LedgerAccount>().HasIndex(la => la.AccountNumber).IsUnique();
            
            modelBuilder.Entity<LedgerTransactionLine>()
                .HasOne(line => line.LedgerAccount)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Customer>().HasIndex(c => c.CustomerId).IsUnique();
            modelBuilder.Entity<Customer>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Customer>().HasMany(c => c.LedgerAccounts).WithOne();
        }
        #endregion

        #region Properties
        public DbSet<CompanySetting> Settings { get; set; }
        public DbSet<Company> Companies { get; set; }

        // Financial Accounting
        public DbSet<ChartOfAccounts> ChartsOfAccounts { get; set; }
        public DbSet<LedgerAccount> LedgerAccounts { get; set; }
        public DbSet<LedgerTransaction> LedgerTransactions { get; set; }
        public DbSet<LedgerTransactionLine> LedgerTransactionLines { get; set; }
        public DbSet<LedgerAccountBalance> LedgerAccountBalances { get; set; }

        public DbSet<Customer> Customers { get; set; }
        #endregion
    }
}