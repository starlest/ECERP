namespace ECERP.Core.Domain.Companies
{
    using Suppliers;

    /// <summary>
    /// Represents a company and supplier relationship
    /// </summary>
    public class CompanySupplier : Entity<int>
    {
        /// <summary>
        /// Gets or sets company identifier
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// Gets or sets supplier identifier
        /// </summary>
        public int SupplierId { get; set; }

        /// <summary>
        /// Gets or sets related company
        /// </summary>
        public virtual Company Company { get; set; }

        /// <summary>
        /// Gets or sets related supplier
        /// </summary>
        public virtual Supplier Supplier { get; set; }
    }
}