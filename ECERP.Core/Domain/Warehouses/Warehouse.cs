namespace ECERP.Core.Domain.Warehouses
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents a warehouse
    /// </summary>
    public class Warehouse : Entity<int>
    {
        /// <summary>
        /// Gets or sets name
        /// </summary>
        [Required, MaxLength(50)]
        public string Name { get; set; }
    }
}