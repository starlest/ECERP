namespace ECERP.Models.Entities.FinancialAccounting
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class LedgerTransactionLine : TransactionLine
    {
        [Required, ForeignKey("LedgerAccount")]
        public int LedgerAccountId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public bool IsDebit { get; set; }

        #region Related Properties
        public virtual LedgerAccount LedgerAccount { get; set; }
        #endregion
    }
}