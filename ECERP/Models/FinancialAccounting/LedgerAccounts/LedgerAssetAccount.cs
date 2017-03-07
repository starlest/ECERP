namespace ECERP.Models.FinancialAccounting.LedgerAccounts
{
    using LedgerGroups;

    public class LedgerAssetAccount : LedgerAccount
    {
        #region Constructor
        public LedgerAssetAccount()
        {
        }
        #endregion

        #region Related Properties
        public virtual LedgerAssetGroup LedgerAssetGroup { get; set; }
        #endregion
    }
}