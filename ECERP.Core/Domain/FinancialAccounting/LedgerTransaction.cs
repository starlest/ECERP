namespace ECERP.Core.Domain.FinancialAccounting
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Represents a ledger transaction
    /// </summary>
    public class LedgerTransaction : Entity<int>
    {
        private DateTime? postingDate;
        private IList<LedgerTransactionLine> _ledgerTransactionLines;

        public LedgerTransaction()
        {
            _ledgerTransactionLines = new List<LedgerTransactionLine>();
        }

        /// <summary>
        /// Gets or sets associated chart of accounts identifier
        /// </summary>
        [Required, ForeignKey("ChartOfAccounts")]
        public int ChartOfAccountsId { get; set; }

        /// <summary>
        /// Gets or sets documentation
        /// </summary>
        [Required, MaxLength(50)]
        public string Documentation { get; set; }

        /// <summary>
        /// Gets or sets description
        /// </summary>
        [Required, MaxLength(500)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets editable
        /// </summary>
        [Required]
        public bool IsEditable { get; set; }

        /// <summary>
        /// Gets or sets is closing entry
        /// </summary>
        [Required]
        public bool IsClosing { get; set; }

        /// <summary>
        /// Gets or sets posting date
        /// </summary>
        [Required]
        public DateTime PostingDate
        {
            get { return postingDate ?? DateTime.Now; }
            set { postingDate = value; }
        }

        /// <summary>
        /// Gets or sets ledger transaction's lines
        /// </summary>
        public virtual IList<LedgerTransactionLine> LedgerTransactionLines
        {
            get { return _ledgerTransactionLines; }
            set { _ledgerTransactionLines = value; }
        }

        /// <summary>
        /// Gets or sets associated chart of accounts
        /// </summary>
        public virtual ChartOfAccounts ChartOfAccounts { get; set; }
    }
}