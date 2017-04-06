namespace ECERP.Core.Domain.Customers
{
    /// <summary>
    /// Represents a many-to-many relationship between customers and companies
    /// </summary>
    public class CustomerCompany
    {
        public int CustomerId { get; set; }

        public int CompanyId { get; set; }

        public int LedgerAccountId { get; set; }
    }
}