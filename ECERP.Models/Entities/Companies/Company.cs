namespace ECERP.Models.Entities.Companies
{
    using System.ComponentModel.DataAnnotations;
    using FinancialAccounting;

    public class Company : IEntity<int>
    {
        #region Constructor
        public Company()
        {
        }
        #endregion

        #region Properties
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }
        #endregion

        #region Related Properties
        public virtual ChartOfAccounts ChartOfAccounts { get; set; }
        #endregion
    }
}