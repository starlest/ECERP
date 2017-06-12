namespace ECERP.Core.Domain.Stocks
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Products;
    using Warehouses;

    /// <summary>
    /// Represents a stock
    /// </summary>
    public class Stock: Entity<int>
    {
        /// <summary>
        /// Gets or sets product identifier
        /// </summary>
        [Required, ForeignKey("Product")]
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets warehouse identifier
        /// </summary>
        [Required, ForeignKey("Warehouse")]
        public int WarehouseId { get; set; }

        /// <summary>
        /// Gets or sets quantity
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets related warehouse
        /// </summary>
        public virtual Warehouse Warehouse { get; set; }

        /// <summary>
        /// Gets or sets related product
        /// </summary>
        public virtual Product Product { get; set; }
    }
}
