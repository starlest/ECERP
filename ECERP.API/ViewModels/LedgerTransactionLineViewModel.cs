namespace ECERP.API.ViewModels
{
    public class LedgerTransactionLineViewModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public bool IsDebit { get; set; }
        public int LedgerAccountId { get; set; }
    }
}
