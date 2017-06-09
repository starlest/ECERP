namespace ECERP.API.ViewModels
{
    using System.Collections.Generic;

    public class ProductViewModel
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string PrimaryUnitName { get; set; }
        public string SecondaryUnitName { get; set; }
        public int QuantityPerPrimaryUnit { get; set; }
        public int QuantityPerSecondaryUnit { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalesPrice { get; set; }
        public bool IsActive { get; set; }
        public string ProductCategory { get; set; }
        public IList<string> Suppliers { get; set; }
    }
}
