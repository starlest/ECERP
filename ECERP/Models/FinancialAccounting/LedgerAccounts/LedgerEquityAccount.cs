namespace ECERP.Models.FinancialAccounting.LedgerAccounts
{
    using LedgerGroups;

    public class LedgerEquityAccount : LedgerAccount
    {
        #region Constructor
        public LedgerEquityAccount()
        {
        }
        #endregion

        #region Related Properties
        public virtual LedgerEquityGroup LedgerEquityGroup { get; set; }
        #endregion
    }
}