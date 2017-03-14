using System;

namespace ECERP.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Entities;

    public abstract class Entity<T> : IEntity<T>
    {
        private DateTime? createdDate;

        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }

        object IEntity.Id
        {
            get { return Id; }
            set { Id = (T) Convert.ChangeType(value, typeof(T)); }
        }

        [Required]
        public DateTime CreatedDate
        {
            get { return createdDate ?? DateTime.UtcNow; }
            set { createdDate = value; }
        }

        public DateTime? ModifiedDate { get; set; }

        [ForeignKey("CreatedBy"), Required]
        public string CreatedById { get; set; }

        [ForeignKey("ModifiedBy")]
        public string ModifiedById { get; set; }

        [Timestamp, Required]
        public byte[] Version { get; set; }
        #endregion

        #region Related Properties
        public ApplicationUser CreatedBy { get; set; }

        public ApplicationUser ModifiedBy { get; set; }
        #endregion
    }
}