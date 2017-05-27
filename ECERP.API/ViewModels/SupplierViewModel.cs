namespace ECERP.API.ViewModels
{
    using System.Collections.Generic;
    using Core.Domain.Companies;

    public class SupplierViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CityViewModel City { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string TaxId { get; set; }
        public bool IsActive { get; set; }
        public IList<string> Companies { get; set; }
    }
}
