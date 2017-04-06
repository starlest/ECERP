using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ECERP.Data;
using ECERP.Core.Domain.FinancialAccounting;

namespace ECERP.API.Migrations
{
    [DbContext(typeof(ECERPDbContext))]
    [Migration("20170331090107_Add fields to ApplicationUser")]
    partial class AddfieldstoApplicationUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ECERP.Core.Domain.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ECERP.Core.Domain.Companies.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("ECERP.Core.Domain.Configuration.CompanySetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CompanyId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(2000);

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("Key")
                        .IsUnique();

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("ECERP.Core.Domain.Customers.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("IsActive");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("ECERP.Core.Domain.FinancialAccounting.ChartOfAccounts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CompanyId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("CurrentLedgerPeriodStartDate");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("CompanyId")
                        .IsUnique();

                    b.ToTable("ChartsOfAccounts");
                });

            modelBuilder.Entity("ECERP.Core.Domain.FinancialAccounting.LedgerAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountNumber");

                    b.Property<int>("ChartOfAccountsId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int?>("CustomerId");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<int>("Group");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDefault");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("Type");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("AccountNumber")
                        .IsUnique();

                    b.HasIndex("ChartOfAccountsId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("LedgerAccounts");
                });

            modelBuilder.Entity("ECERP.Core.Domain.FinancialAccounting.LedgerAccountBalance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Balance1");

                    b.Property<decimal>("Balance10");

                    b.Property<decimal>("Balance11");

                    b.Property<decimal>("Balance12");

                    b.Property<decimal>("Balance2");

                    b.Property<decimal>("Balance3");

                    b.Property<decimal>("Balance4");

                    b.Property<decimal>("Balance5");

                    b.Property<decimal>("Balance6");

                    b.Property<decimal>("Balance7");

                    b.Property<decimal>("Balance8");

                    b.Property<decimal>("Balance9");

                    b.Property<decimal>("BeginningBalance");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("LedgerAccountId");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.HasIndex("LedgerAccountId");

                    b.ToTable("LedgerAccountBalances");
                });

            modelBuilder.Entity("ECERP.Core.Domain.FinancialAccounting.LedgerTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ChartOfAccountsId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("Documentation")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<DateTime>("PostingDate");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("ChartOfAccountsId");

                    b.ToTable("LedgerTransactions");
                });

            modelBuilder.Entity("ECERP.Core.Domain.FinancialAccounting.LedgerTransactionLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDebit");

                    b.Property<int>("LedgerAccountId");

                    b.Property<int>("LedgerTransactionId");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("LedgerAccountId");

                    b.HasIndex("LedgerTransactionId");

                    b.ToTable("LedgerTransactionLines");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictApplication", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClientId");

                    b.Property<string>("ClientSecret");

                    b.Property<string>("DisplayName");

                    b.Property<string>("LogoutRedirectUri");

                    b.Property<string>("RedirectUri");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("ClientId")
                        .IsUnique();

                    b.ToTable("OpenIddictApplications");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictAuthorization", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationId");

                    b.Property<string>("Scope");

                    b.Property<string>("Subject");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("OpenIddictAuthorizations");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictScope", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("Id");

                    b.ToTable("OpenIddictScopes");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictToken", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationId");

                    b.Property<string>("AuthorizationId");

                    b.Property<string>("Subject");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("AuthorizationId");

                    b.ToTable("OpenIddictTokens");
                });

            modelBuilder.Entity("ECERP.Core.Domain.Configuration.CompanySetting", b =>
                {
                    b.HasOne("ECERP.Core.Domain.Companies.Company", "Company")
                        .WithMany("CompanySettings")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ECERP.Core.Domain.FinancialAccounting.ChartOfAccounts", b =>
                {
                    b.HasOne("ECERP.Core.Domain.Companies.Company", "Company")
                        .WithOne("ChartOfAccounts")
                        .HasForeignKey("ECERP.Core.Domain.FinancialAccounting.ChartOfAccounts", "CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ECERP.Core.Domain.FinancialAccounting.LedgerAccount", b =>
                {
                    b.HasOne("ECERP.Core.Domain.FinancialAccounting.ChartOfAccounts", "ChartOfAccounts")
                        .WithMany("LedgerAccounts")
                        .HasForeignKey("ChartOfAccountsId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ECERP.Core.Domain.Customers.Customer")
                        .WithMany("LedgerAccounts")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("ECERP.Core.Domain.FinancialAccounting.LedgerAccountBalance", b =>
                {
                    b.HasOne("ECERP.Core.Domain.FinancialAccounting.LedgerAccount", "LedgerAccount")
                        .WithMany()
                        .HasForeignKey("LedgerAccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ECERP.Core.Domain.FinancialAccounting.LedgerTransaction", b =>
                {
                    b.HasOne("ECERP.Core.Domain.FinancialAccounting.ChartOfAccounts", "ChartOfAccounts")
                        .WithMany()
                        .HasForeignKey("ChartOfAccountsId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ECERP.Core.Domain.FinancialAccounting.LedgerTransactionLine", b =>
                {
                    b.HasOne("ECERP.Core.Domain.FinancialAccounting.LedgerAccount", "LedgerAccount")
                        .WithMany()
                        .HasForeignKey("LedgerAccountId");

                    b.HasOne("ECERP.Core.Domain.FinancialAccounting.LedgerTransaction", "LedgerTransaction")
                        .WithMany("LedgerTransactionLines")
                        .HasForeignKey("LedgerTransactionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ECERP.Core.Domain.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ECERP.Core.Domain.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ECERP.Core.Domain.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictAuthorization", b =>
                {
                    b.HasOne("OpenIddict.Models.OpenIddictApplication", "Application")
                        .WithMany("Authorizations")
                        .HasForeignKey("ApplicationId");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictToken", b =>
                {
                    b.HasOne("OpenIddict.Models.OpenIddictApplication", "Application")
                        .WithMany("Tokens")
                        .HasForeignKey("ApplicationId");

                    b.HasOne("OpenIddict.Models.OpenIddictAuthorization", "Authorization")
                        .WithMany("Tokens")
                        .HasForeignKey("AuthorizationId");
                });
        }
    }
}
