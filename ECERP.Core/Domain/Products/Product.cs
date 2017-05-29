namespace ECERP.Core.Domain.Products
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Represents a product
    /// </summary>
    public class Product : Entity<int>
    {
        public Product()
        {
            IsActive = true;
        }

        /// <summary>
        /// Gets or sets product identifier
        /// </summary>
        [Required, MaxLength(50)]
        public string ProductId { get; set; }

        /// <summary>
        /// Gets or sets name
        /// </summary>
        [Required, MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets product category identifier
        /// </summary>
        [Required, ForeignKey("ProductCategory")]
        public int ProductCategoryId { get; set; }

        /// <summary>
        /// Gets or sets primary unit name
        /// </summary>
        [Required, MaxLength(10)]
        public string PrimaryUnitName { get; set; }

        /// <summary>
        /// Gets or sets secondary unit name
        /// </summary>
        [Required, MaxLength(10)]
        public string SecondaryUnitName { get; set; }

        /// <summary>
        /// Gets or sets the quantity per primary unit
        /// </summary>
        [Required]
        public int QuantityPerPrimaryUnit { get; set; }

        /// <summary>
        /// Gets or sets the quantity per secondary unit
        /// </summary>
        [Required]
        public int QuantityPerSecondaryUnit { get; set; }

        /// <summary>
        /// Gets or sets the sales price
        /// </summary>
        [Required]
        public decimal SalesPrice { get; set; }

        /// <summary>
        /// Gets or sets purchase price
        /// </summary>
        [Required]
        public decimal PurchasePrice { get; set; }

        /// <summary>
        /// Gets or sets active
        /// </summary>
        [Required]
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets related product category
        /// </summary>
        public virtual ProductCategory ProductCategory { get; set; }
    }
}