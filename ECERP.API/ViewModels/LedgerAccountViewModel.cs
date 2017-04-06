namespace ECERP.API.ViewModels
{
    public class LedgerAccountViewModel
    {
        public int Id { get; set; }
        public int AccountNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Group { get; set; }
        public string IsActive { get; set; }
        public string IsHidden { get; set; }
        public string IsDefault { get; set; }
        public string CreatedDate { get; set; }
        public string Company { get; set; }
        public int ChartOfAccountsId { get; set; }
    }
}
