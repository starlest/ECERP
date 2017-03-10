namespace ECERP.Models.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class ApplicationUser : IdentityUser, IEntity<string>
    {
        #region Constructor
        public ApplicationUser()
        {
        }
        #endregion

        #region Properties
        public string DisplayName { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }


        [MaxLength(50)]
        public string MiddleName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required, MaxLength(50)]
        public string CreatedBy { get; set; }

        [Required, MaxLength(50)]
        public string ModifiedBy { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; }
        #endregion
    }
}