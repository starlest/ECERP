﻿namespace ECERP.Core.Domain.Products
{
    using Suppliers;

    public class ProductSupplier : Entity<int>
    {
        /// <summary>
        /// Gets or sets product identifier
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets supplier identifier
        /// </summary>
        public int SupplierId { get; set; }

        /// <summary>
        /// Gets or sets purchase price
        /// </summary>
        public decimal PurchasePrice { get; set; }

        /// <summary>
        /// Gets or sets related product
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        /// Gets or sets related supplier
        /// </summary>
        public virtual Supplier Supplier { get; set; }
    }
}