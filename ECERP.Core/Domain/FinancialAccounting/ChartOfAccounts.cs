namespace ECERP.Core.Domain.FinancialAccounting
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Companies;
    using Core;
    using Models.Entities.FinancialAccounting;

    /// <summary>
    /// Represents a chart of accounts
    /// </summary>
    public class ChartOfAccounts : Entity<int>
    {
        #region Constructor 
        public ChartOfAccounts()
        {
        }
        #endregion

        #region Properties
        [Required, ForeignKey("Company")]
        public int CompanyId { get; set; }
        #endregion

        #region Navigational Properties
        public virtual List<LedgerAccount> LedgerAccounts { get; set; }

        public virtual Company Company { get; set; }
        #endregion
    }
}