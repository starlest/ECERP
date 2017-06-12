namespace ECERP.Core.Domain.Suppliers
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Companies;
    using FinancialAccounting;

    /// <summary>
    /// Represents a supplier subscription
    /// </summary>
    public class SupplierSubscription : Entity<int>
    {
        public SupplierSubscription()
        {
            IsActive = true;
        }

        /// <summary>
        /// Gets or sets related supplier identifier
        /// </summary>
        [Required, ForeignKey("Supplier")]
        public int SupplierId { get; set; }

        /// <summary>
        /// Gets or sets related company identifier
        /// </summary>
        [Required, ForeignKey("Company")]
        public int CompanyId { get; set; }

        /// <summary>
        /// Gets or sets related ledger account identifier
        /// </summary>
        [Required, ForeignKey("LedgerAccount")]
        public int LedgerAccountId { get; set; }

        /// <summary>
        /// Gets or sets active
        /// </summary>
        [Required]
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets related company
        /// </summary>
        public virtual Company Company { get; set; }

        /// <summary>
        /// Gets or sets related supplier
        /// </summary>
        public virtual Supplier Supplier { get; set; }

        /// <summary>
        /// Gets or sets related ledger account
        /// </summary>
        public virtual LedgerAccount LedgerAccount { get; set; }
    }
}