namespace ECERP.Core.Domain.Suppliers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Cities;
    using Companies;

    /// <summary>
    /// Represents a supplier
    /// </summary>
    public class Supplier : Entity<int>
    {
        private IList<CompanySupplier> _companySuppliers;

        public Supplier()
        {
            IsActive = true;
            _companySuppliers = new List<CompanySupplier>();
        }

        /// <summary>
        /// Gets or sets name
        /// </summary>
        [Required, MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets city identifier
        /// </summary>
        [Required, ForeignKey("City")]
        public int CityId { get; set; }

        /// <summary>
        /// Gets or sets address
        /// </summary>
        [Required, MaxLength(500)]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets contact number
        /// </summary>
        [Required, MaxLength(50)]
        public string ContactNumber { get; set; }

        /// <summary>
        /// Gets or sets tax identification
        /// </summary>
        [Required, MaxLength(50)]
        public string TaxId { get; set; }

        /// <summary>
        /// Gets or sets is active
        /// </summary>
        [Required]
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets related city
        /// </summary>
        public virtual City City { get; set; }

        /// <summary>
        /// Gets or sets related companies
        /// </summary>
        public virtual IList<CompanySupplier> CompanySuppliers
        {
            get { return _companySuppliers; }
            set { _companySuppliers = value; }
        }
    }
}