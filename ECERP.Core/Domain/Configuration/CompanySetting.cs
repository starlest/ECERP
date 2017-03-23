namespace ECERP.Core.Domain.Configuration
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Companies;
    using Models;

    /// <summary>
    /// Represents a setting
    /// </summary>
    public class CompanySetting : Entity<int>
    {
        #region Constructors
        public CompanySetting()
        {
        }

        #endregion

        /// <summary>
        /// Gets or sets the key
        /// </summary>
        [Required, MaxLength(200)]
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        [Required, MaxLength(2000)]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the company id for which this setting is valid.
        /// </summary>
        [Required, ForeignKey("Company")]
        public int CompanyId { get; set; }

        #region Navigational Properties
        public virtual Company Company { get; set; }
        #endregion
    }
}