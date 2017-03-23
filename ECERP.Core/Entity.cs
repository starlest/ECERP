namespace ECERP.Core
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Reflection;

    /// <summary>
    /// Base class for entities
    /// </summary>
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
            get { return createdDate ?? DateTime.UtcNow; }
            set { createdDate = value; }
        }

        public DateTime? ModifiedDate { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }
        #endregion

        #region Equality
        public override bool Equals(object obj)
        {
            return Equals(obj as Entity<T>);
        }

        private static bool IsTransient(Entity<T> obj)
        {
            return obj != null && Equals(obj.Id, default(T));
        }

        private Type GetUnproxiedType()
        {
            return GetType();
        }

        public virtual bool Equals(Entity<T> other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (IsTransient(this) || IsTransient(other) || !Equals(Id, other.Id)) return false;
            var otherType = other.GetUnproxiedType();
            var thisType = GetUnproxiedType();
            return thisType.IsAssignableFrom(otherType) ||
                   otherType.IsAssignableFrom(thisType);
        }

        public override int GetHashCode()
        {
            return Equals(Id, default(T)) ? base.GetHashCode() : Id.GetHashCode();
        }

        public static bool operator ==(Entity<T> x, Entity<T> y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(Entity<T> x, Entity<T> y)
        {
            return !(x == y);
        }
        #endregion
    }
}