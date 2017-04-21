namespace ECERP.Core.Domain.FinancialAccounting
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Companies;
    using Core;

    /// <summary>
    /// Represents a chart of accounts
    /// </summary>
    public class ChartOfAccounts : Entity<int>
    {
        public ChartOfAccounts()
        {
            LedgerAccounts = new List<LedgerAccount>();
            CurrentLedgerPeriodStartDate = DateTime.Now.Date.AddDays(-DateTime.Now.Day + 1);
        }

        /// <summary>
        /// Gets or sets the associated company identifier
        /// </summary>
        [Required, ForeignKey("Company")]
        public int CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the current ledger period start date
        /// </summary>
        [Required]
        public DateTime CurrentLedgerPeriodStartDate { get; set; }

        /// <summary>
        /// Gets or sets ledger accounts
        /// </summary>
        public IList<LedgerAccount> LedgerAccounts { get; set; }

        /// <summary>
        /// Gets or sets the associated company
        /// </summary>
        public Company Company { get; set; }
    }
}