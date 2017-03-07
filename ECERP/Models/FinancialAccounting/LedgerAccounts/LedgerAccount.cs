namespace ECERP.Models.FinancialAccounting.LedgerAccounts
{
    public class LedgerAccount
    {
        #region Constructor
        public LedgerAccount()
        {
        }
        #endregion

        #region Properties
        public int Id { get; set; }
        public int AccountNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsDebitNormal { get; set; }
        #endregion
    }
}