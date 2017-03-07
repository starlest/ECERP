namespace ECERP.Models.FinancialAccounting.ChartsOfAccounts
{
    using System.Collections.Generic;
    using Companies;
    using LedgerAccounts;

    public class ChartOfAccounts
    {
        #region Constructor 
        public ChartOfAccounts()
        {
        }
        #endregion

        #region Properties
        public int Id { get; set; }
        public int CompanyId { get; set; }
        #endregion

        #region Related Properties
        public virtual List<LedgerAccount> LedgerAccounts { get; set; }

        public virtual Company Company { get; set; }
        #endregion
    }
}