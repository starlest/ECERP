namespace ECERP.Models.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Companies;

    public class SystemParameter : Entity<int>
    {
        [Required, MaxLength(50)]
        public string Key { get; set; }

        [Required, MaxLength(50)]
        public string Value { get; set; }

        [Required, ForeignKey("Company")]
        public int CompanyId { get; set; }

        #region Related Properties
        public virtual Company Company { get; set; }
        #endregion
    }
}