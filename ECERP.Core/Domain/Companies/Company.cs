namespace ECERP.Core.Domain.Companies
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Configuration;
    using FinancialAccounting;
    using Models;
    using Models.Entities.FinancialAccounting;

    /// <summary>
    /// Represents a company
    /// </summary>
    public class Company : Entity<int>
    {
        #region Fields
        private ICollection<CompanySetting> _companySettings;
        private ChartOfAccounts _chartOfAccounts;

        private const string CurrentLedgerPeriodStartDateKey = "CurrentLedgerPeriodStartDate";
        #endregion

        #region Constructors
        public Company()
        {
            _chartOfAccounts = new ChartOfAccounts();
            _companySettings = new List<CompanySetting>();
            var currentLedgerPeriodStartDateValue = DateTime.UtcNow.AddDays(-DateTime.UtcNow.Day + 1).Date;
            var companySetting = new CompanySetting
            {
                Key = CurrentLedgerPeriodStartDateKey,
                Value = CommonHelper.To<string>(currentLedgerPeriodStartDateValue)
            };
            _companySettings.Add(companySetting);
        }
        #endregion

        /// <summary>
        /// Gets or sets names
        /// </summary>
        [Required, MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Default Chart of Accounts
        /// </summary>
        public virtual ChartOfAccounts ChartOfAccounts
        {
            get { return _chartOfAccounts; }
            set { _chartOfAccounts = value; }
        }

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