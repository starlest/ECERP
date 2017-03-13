namespace ECERP.Models.Entities.FinancialAccounting
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class LedgerAccount : IEntity<int>, IEquatable<LedgerAccount>
    {
        #region Constructor
        public LedgerAccount()
        {
        }
        #endregion

        #region Properties
        [Key]
        public int Id { get; set; }

        [Required]
        public int AccountNumber { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required, MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public bool IsActive { get; set; }

        // Default accounts aren't deletable
        [Required]
        public bool IsDefault { get; set; }

        [Required]
        public LedgerAccountType Type { get; set; }

        [Required]
        public LedgerAccountGroup Group { get; set; }

        [Required, ForeignKey("ChartOfAccounts")]
        public int ChartOfAccountsId { get; set; }
        #endregion

        #region Related Properties
        public virtual ChartOfAccounts ChartOfAccounts { get; set; }
        #endregion

        #region Equality
        public bool Equals(LedgerAccount other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }
        #endregion
    }
}