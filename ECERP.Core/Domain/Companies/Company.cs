namespace ECERP.Core.Domain.Companies
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Configuration;
    using FinancialAccounting;

    /// <summary>
    /// Represents a company
    /// </summary>
    public class Company : Entity<int>
    {
        private ICollection<CompanySetting> _companySettings;

        public Company()
        {
            _companySettings = new List<CompanySetting>();
            ChartOfAccounts = new ChartOfAccounts();
        }

        /// <summary>
        /// Gets or sets names
        /// </summary>
        [Required, MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Default Chart of Accounts
        /// </summary>
        public ChartOfAccounts ChartOfAccounts { get; set; }

        /// <summary>
        /// Gets or sets company's settings
        /// </summary>
        public virtual ICollection<CompanySetting> CompanySettings
        {
            get { return _companySettings; }
            set { _companySettings = value; }
        }
    }
}