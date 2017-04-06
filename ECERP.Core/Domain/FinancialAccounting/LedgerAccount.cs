namespace ECERP.Core.Domain.FinancialAccounting
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Core;

    /// <summary>
    /// Represents a ledger account
    /// </summary>
    public class LedgerAccount : Entity<int>
    {
        public LedgerAccount()
        {
            IsActive = true;
        }

        /// <summary>
        /// Gets or sets account number
        /// </summary>
        [Required]
        public int AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets name
        /// </summary>
        [Required, MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets description
        /// </summary>
        [Required, MaxLength(500)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets hidden from users
        /// </summary>
        [Required]
        public bool IsHidden { get; set; }

        /// <summary>
        /// Gets or sets if account is active
        /// </summary>
        [Required]
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets if account is created by default (not deletable)
        /// </summary>
        [Required]
        public bool IsDefault { get; set; }

        /// <summary>
        /// Gets or sets account's type
        /// </summary>
        [Required]
        public LedgerAccountType Type { get; set; }

        /// <summary>
        /// Gets or sets account's group
        /// </summary>
        [Required]
        public LedgerAccountGroup Group { get; set; }

        /// <summary>
        /// Gets or sets related chart of accounts identifier
        /// </summary>
        [Required, ForeignKey("ChartOfAccounts")]
        public int ChartOfAccountsId { get; set; }

        /// <summary>
        /// Gets or sets related chart of accounts
        /// </summary>
        public virtual ChartOfAccounts ChartOfAccounts { get; set; }
    }
}