namespace ECERP.Models.Entities.Companies
{
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using System.ComponentModel.DataAnnotations;

    public class Company : IEntity<int>
    {
        #region Constructor
        public Company()
        {
        }
        #endregion

        #region Properties
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        #endregion
    }
}