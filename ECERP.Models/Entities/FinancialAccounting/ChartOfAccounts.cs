namespace ECERP.Models.Entities.FinancialAccounting
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Companies;

    public class ChartOfAccounts : IEntity<int>, IEquatable<ChartOfAccounts>
    {
        #region Constructor 
        public ChartOfAccounts()
        {
        }
        #endregion

        #region Properties
        [Key]
        public int Id { get; set; }

        [Required, ForeignKey("Company")]
        public int CompanyId { get; set; }
        #endregion

        #region Related Properties
        public virtual List<LedgerAccount> LedgerAccounts { get; set; }

        public virtual Company Company { get; set; }
        #endregion

        public bool Equals(ChartOfAccounts other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            var equals = other.Id == Id &&
                         other.LedgerAccounts.Count == LedgerAccounts.Count;
            return equals && LedgerAccounts.All(ledgerAccount => other.LedgerAccounts.Contains(ledgerAccount));
        }
    }
}