namespace ECERP.Models.FinancialAccounting.LedgerAccounts
{
    using LedgerGroups;

    public class LedgerLiabilityAccount : LedgerAccount
    {
        #region Constructor
        public LedgerLiabilityAccount()
        {
        }
        #endregion

        #region Related Properties
        public virtual LedgerLiabilityGroup LedgerLiabilityGroup { get; set; }
        #endregion
    }
}
