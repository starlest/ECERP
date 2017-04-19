namespace ECERP.API.ViewModels
{
    using System.Collections.Generic;

    public class LedgerTransactionViewModel
    {
        public int Id { get; set; }
        public string Documentation { get; set; }
        public string Description { get; set; }
        public string PostingDate { get; set; }
        public string CreatedDate { get; set; }
        public bool IsEditable { get; set; }
        public int ChartOfAccountsId { get; set; }
        public IList<LedgerTransactionLineViewModel> LedgerTransactionLines { get; set; }
    }
}
