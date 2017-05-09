namespace ECERP.Core.Domain.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Represents a product
    /// </summary>
    public class Product : Entity<int>
    {
        private IList<ProductSupplier> _productSuppliers;

        public Product()
        {
            _productSuppliers = new List<ProductSupplier>();
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
        /// Gets or sets unit name
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
        /// Gets or sets active
        /// </summary>
        [Required]
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets related product category
        /// </summary>
        public virtual ProductCategory ProductCategory { get; set; }

        /// <summary>
        /// Gets or sets related product suppliers
        /// </summary>
        public virtual IList<ProductSupplier> ProductSuppliers
        {
            get { return _productSuppliers; }
            set { _productSuppliers = value; }
        }
    }
}