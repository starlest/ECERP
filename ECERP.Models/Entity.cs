using System;

namespace ECERP.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public abstract class Entity<T> : IEntity<T>, IEquatable<Entity<T>>
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

        #region Equality
        public bool Equals(Entity<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return createdDate.Equals(other.createdDate) && EqualityComparer<T>.Default.Equals(Id, other.Id) &&
                   ModifiedDate.Equals(other.ModifiedDate) && string.Equals(CreatedBy, other.CreatedBy) &&
                   string.Equals(ModifiedBy, other.ModifiedBy) && Equals(Version, other.Version);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((Entity<T>) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = createdDate.GetHashCode();
                hashCode = (hashCode * 397) ^ EqualityComparer<T>.Default.GetHashCode(Id);
                hashCode = (hashCode * 397) ^ ModifiedDate.GetHashCode();
                hashCode = (hashCode * 397) ^ (CreatedBy?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (ModifiedBy?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Version?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
        #endregion
    }
}