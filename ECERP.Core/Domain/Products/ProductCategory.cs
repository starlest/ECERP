namespace ECERP.Core.Domain.Products
{
    using System.ComponentModel.DataAnnotations;

    public class ProductCategory : Entity<int>
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }
    }
}