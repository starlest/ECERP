namespace ECERP.Data
{
    using Core.Domain;
    using Core.Domain.Cities;
    using Core.Domain.Companies;
    using Core.Domain.Configuration;
    using Core.Domain.Customers;
    using Core.Domain.FinancialAccounting;
    using Core.Domain.Products;
    using Core.Domain.Suppliers;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;

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

            modelBuilder.Entity<City>().HasIndex(c => c.Name).IsUnique();

            modelBuilder.Entity<Company>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<CompanySupplier>().HasIndex("CompanyId", "SupplierId").IsUnique();
            modelBuilder.Entity<CompanySetting>().HasIndex(p => p.Key).IsUnique();

            modelBuilder.Entity<ChartOfAccounts>().ToTable("ChartsOfAccounts");
            modelBuilder.Entity<ChartOfAccounts>().HasOne(coa => coa.Company).WithOne(c => c.ChartOfAccounts);

            modelBuilder.Entity<LedgerAccount>().HasIndex(la => la.AccountNumber).IsUnique();
            modelBuilder.Entity<LedgerAccount>().HasIndex("Name", "ChartOfAccountsId").IsUnique();

            modelBuilder.Entity<LedgerTransaction>()
                .HasMany(lt => lt.LedgerTransactionLines)
                .WithOne(l => l.LedgerTransaction);

            modelBuilder.Entity<LedgerTransactionLine>()
                .HasOne(line => line.LedgerAccount)
                .WithMany(account => account.LedgerTransactionLines)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Supplier>().HasIndex(s => s.Name).IsUnique();

            modelBuilder.Entity<Product>().HasIndex(p => p.Name).IsUnique();
            modelBuilder.Entity<Product>().HasIndex(p => p.ProductId).IsUnique();
            modelBuilder.Entity<ProductCategory>().HasIndex(p => p.Name).IsUnique();
            modelBuilder.Entity<ProductSupplier>().HasIndex("ProductId", "SupplierId").IsUnique();

            modelBuilder.Entity<Customer>().HasIndex(c => c.CustomerId).IsUnique();
            modelBuilder.Entity<Customer>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Customer>().HasMany(c => c.LedgerAccounts).WithOne();
        }
        #endregion

        #region Properties
        public DbSet<CompanySetting> Settings { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanySupplier> CompanySuppliers { get; set; }

        public DbSet<City> Cities { get; set; }

        // Entities
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        // Financial Accounting
        public DbSet<ChartOfAccounts> ChartsOfAccounts { get; set; }
        public DbSet<LedgerAccount> LedgerAccounts { get; set; }
        public DbSet<LedgerAccountBalance> LedgerAccountBalances { get; set; }
        public DbSet<LedgerTransaction> LedgerTransactions { get; set; }
        public DbSet<LedgerTransactionLine> LedgerTransactionLines { get; set; }

        // Inventory
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductSupplier> ProductSuppliers { get; set; }
        #endregion
    }
}