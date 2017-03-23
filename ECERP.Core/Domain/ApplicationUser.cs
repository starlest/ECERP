namespace ECERP.Models.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Core;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class ApplicationUser : IdentityUser, IEntity<string>
    {
        private DateTime? createdDate;

        #region Properties
        object IEntity.Id
        {
            get { return Id; }
            set { Id = (string) Convert.ChangeType(value, typeof(string)); }
        }

        [Required]
        public DateTime CreatedDate
        {
            get { return createdDate ?? DateTime.Now; }
            set { createdDate = value; }
        }

        public DateTime? ModifiedDate { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }
        #endregion
    }
}