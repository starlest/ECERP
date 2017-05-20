namespace ECERP.Core.Domain.Suppliers
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Cities;

    /// <summary>
    /// Represents a supplier
    /// </summary>
    public class Supplier : Entity<int>
    {
        public Supplier()
        {
            IsActive = true;
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
    }
}