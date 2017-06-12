namespace ECERP.Core.Domain.Products
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Suppliers;

    /// <summary>
    /// Represents a product subscription
    /// </summary>
    public class ProductSubscription : Entity<int>
    {
        public ProductSubscription()
        {
            IsActive = true;
        }

        /// <summary>
        /// Gets or sets related product identifier
        /// </summary>
        [Required, ForeignKey("Product")]
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets related companySupplier identifier
        /// </summary>
        [Required, ForeignKey("SupplierSubscription")]
        public int SupplierSubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets related product
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        /// Gets or sets related companySupplier
        /// </summary>
        public virtual SupplierSubscription SupplierSubscription { get; set; }
    }
}