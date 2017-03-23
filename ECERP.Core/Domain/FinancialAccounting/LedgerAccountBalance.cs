namespace ECERP.Models.Entities.FinancialAccounting
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class LedgerAccountBalance : AccountBalance<decimal>
    {
        #region Properties
        [ForeignKey("LedgerAccount")]
        public int LedgerAccountId { get; set; }
        #endregion

        #region Related Properties
        public virtual LedgerAccount LedgerAccount { get; set; }
        #endregion
    }
}