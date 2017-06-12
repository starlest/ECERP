namespace ECERP.Core.Domain.PurchaseTransactions
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Suppliers;

    /// <summary>
    /// Represents a purchase transaction
    /// </summary>
    public class PurchaseTransaction : Entity<int>
    {
        /// <summary>
        /// Gets or sets supplier's invoice identifier
        /// </summary>
        [MaxLength(50)]
        public string InvoiceId { get; set; }

        /// <summary>
        /// Gets or sets delivery order identifier
        /// </summary>
        [MaxLength(50)]
        public string DeliveryOrderId { get; set; }

        /// <summary>
        /// Gets or sets supplier's invoice date
        /// </summary>
        public DateTime InvoiceDate { get; set; }

        /// <summary>
        /// Gets or sets supplier's invoice due date
        /// </summary>
        public DateTime InvoiceDueDate { get; set; }

        /// <summary>
        /// Gets or sets discount
        /// </summary>
        [Required]
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets or sets tax
        /// </summary>
        [Required]
        public decimal Tax { get; set; }

        /// <summary>
        /// Gets or sets note
        /// </summary>
        [MaxLength(500)]
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets related companySupplier identifier
        /// </summary>
        [Required, ForeignKey("CompanySupplier")]
        public int CompanySupplierId { get; set; }

        /// <summary>
        /// Gets or sets related companySupplier
        /// </summary>
        public virtual SupplierSubscription CompanySupplier { get; set; }
    }
}