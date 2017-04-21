namespace ECERP.Core.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;
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

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public DateTime CreatedDate
        {
            get { return createdDate ?? DateTime.Now; }
            set { createdDate = value; }
        }

        public DateTime? ModifiedDate { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }
        #endregion

        #region Equality
        public override bool Equals(object obj)
        {
            return Equals(obj as ApplicationUser);
        }

        private static bool IsTransient(ApplicationUser obj)
        {
            return obj != null && Equals(obj.Id, default(string));
        }

        private Type GetUnproxiedType()
        {
            return GetType();
        }

        public virtual bool Equals(ApplicationUser other)
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
            return Equals(Id, default(string)) ? base.GetHashCode() : Id.GetHashCode();
        }

        public static bool operator ==(ApplicationUser x, ApplicationUser y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(ApplicationUser x, ApplicationUser y)
        {
            return !(x == y);
        }
        #endregion
    }
}