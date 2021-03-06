﻿namespace ECERP.Core.Domain.FinancialAccounting
{
    using Core;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Represents a ledger transaction line
    /// </summary>
    public class LedgerTransactionLine : Entity<int>
    {
        /// <summary>
        /// Gets or sets the associated ledger transaction identifier
        /// </summary>
        [ForeignKey("LedgerTransaction")]
        public int LedgerTransactionId { get; set; }

        /// <summary>
        /// Gets or sets the associated ledger account identifier
        /// </summary>
        [ForeignKey("LedgerAccount")]
        public int LedgerAccountId { get; set; }

        /// <summary>
        /// Gets or sets amount
        /// </summary>
        [Required]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets if is debit
        /// </summary>
        [Required]
        public bool IsDebit { get; set; }

        /// <summary>
        /// Gets or sets the associated ledger account
        /// </summary>
        public virtual LedgerAccount LedgerAccount { get; set; }

        /// <summary>
        /// Gets or sets the associated ledger transaction
        /// </summary>
        public virtual LedgerTransaction LedgerTransaction { get; set; }
    }
}