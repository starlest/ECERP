namespace ECERP.Models.Entities.FinancialAccounting
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Companies;

    public class ChartOfAccounts : IEntity<int>
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
    }
}