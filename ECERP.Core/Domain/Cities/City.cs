namespace ECERP.Core.Domain.Cities
{
    using System.ComponentModel.DataAnnotations;

    public class City : Entity<int>
    {
        /// <summary>
        /// Gets or sets name
        /// </summary>
        [Required, MaxLength(100)]
        public string Name { get; set; }
    }
}