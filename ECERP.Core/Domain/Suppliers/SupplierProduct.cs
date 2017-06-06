namespace ECERP.Core.Domain.Suppliers
{
    using Products;

    public class SupplierProduct : Entity<int>
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
        /// Gets or sets related product
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        /// Gets or sets related supplier
        /// </summary>
        public virtual Supplier Supplier { get; set; }
    }
}