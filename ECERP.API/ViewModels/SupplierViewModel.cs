namespace ECERP.API.ViewModels
{
    public class SupplierViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CityViewModel City { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
