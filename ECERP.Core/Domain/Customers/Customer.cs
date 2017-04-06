namespace ECERP.Core.Domain.Customers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using FinancialAccounting;

    /// <summary>
    /// Represents a customer
    /// </summary>
    public class Customer : Entity<int>
    {
        public Customer()
        {
            IsActive = true;
        }

        /// <summary>
        /// Gets or sets customer identifier
        /// </summary>
        [Required, MaxLength(50)]
        public string CustomerId { get; set; }

        /// <summary>
        /// Gets or sets name
        /// </summary>
        [Required, MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets address
        /// </summary>
        [Required, MaxLength(500)]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets contact number
        /// </summary>
        [Required, MaxLength(50)]
        public string ContactNumber { get; set; }

        /// <summary>
        /// Gets or sets if the customer is active
        /// </summary>
        [Required]
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets associated ledger accounts
        /// </summary>
        public IList<LedgerAccount> LedgerAccounts { get; set; }
    }
}