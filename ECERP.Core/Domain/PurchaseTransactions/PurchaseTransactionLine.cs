namespace ECERP.Core.Domain.PurchaseTransactions
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Products;

    /// <summary>
    /// Represents a purchase transaction line
    /// </summary>
    public class PurchaseTransactionLine : Entity<int>
    {
        /// <summary>
        /// Gets or sets related purchase transaction identifier
        /// </summary>
        [Required, ForeignKey("PurchaseTransaction")]
        public int PurchaseTransactionId { get; set; }

        /// <summary>
        /// Gets or sets related product identifier
        /// </summary>
        [Required, ForeignKey("Product")]
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets quantity
        /// </summary>
        [Required]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets price
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets related purchase transaction
        /// </summary>
        public virtual PurchaseTransaction PurchaseTransaction { get; set; }

        /// <summary>
        /// Gets or sets related product
        /// </summary>
        public virtual Product Product { get; set; }
    }
}
