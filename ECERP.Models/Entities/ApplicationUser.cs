﻿namespace ECERP.Models.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
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

        [ForeignKey("CreatedBy"), Required]
        public string CreatedById { get; set; }

        [ForeignKey("ModifiedBy")]
        public string ModifiedById { get; set; }

        [Required]
        public DateTime CreatedDate
        {
            get { return createdDate ?? DateTime.UtcNow; }
            set { createdDate = value; }
        }

        public DateTime? ModifiedDate { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }
        #endregion

        #region Related Properties
        public ApplicationUser CreatedBy { get; set; }

        public ApplicationUser ModifiedBy { get; set; }
        #endregion
    }
}