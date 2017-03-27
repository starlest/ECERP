namespace ECERP.Core.Domain.FinancialAccounting
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Represents a ledger account balance
    /// </summary>
    public class LedgerAccountBalance : Entity<int>
    {
        /// <summary>
        /// Ledger account identifier associated with this balance
        /// </summary>
        [Required, ForeignKey("LedgerAccount")]
        public int LedgerAccountId { get; set; }

        /// <summary>
        /// Account balance period year
        /// </summary>
        [Required]
        public int Year { get; set; }

        /// <summary>
        /// Period year beginning balance
        /// </summary>
        [Required]
        public decimal BeginningBalance { get; set; }

        /// <summary>
        /// Period year January balance
        /// </summary>
        [Required]
        public decimal Balance1 { get; set; }

        /// <summary>
        /// Period year February balance
        /// </summary>
        [Required]
        public decimal Balance2 { get; set; }

        /// <summary>
        /// Period year March balance
        /// </summary>
        [Required]
        public decimal Balance3 { get; set; }

        /// <summary>
        /// Period year April balance
        /// </summary>
        [Required]
        public decimal Balance4 { get; set; }

        /// <summary>
        /// Period year May balance
        /// </summary>
        [Required]
        public decimal Balance5 { get; set; }

        /// <summary>
        /// Period year June balance
        /// </summary>
        [Required]
        public decimal Balance6 { get; set; }

        /// <summary>
        /// Period year July balance
        /// </summary>
        [Required]
        public decimal Balance7 { get; set; }

        /// <summary>
        /// Period year August balance
        /// </summary>
        [Required]
        public decimal Balance8 { get; set; }

        /// <summary>
        /// Period year September balance
        /// </summary>
        [Required]
        public decimal Balance9 { get; set; }

        /// <summary>
        /// Period year October balance
        /// </summary>
        [Required]
        public decimal Balance10 { get; set; }

        /// <summary>
        /// Period year November balance
        /// </summary>
        [Required]
        public decimal Balance11 { get; set; }

        /// <summary>
        /// Period year December balance
        /// </summary>
        [Required]
        public decimal Balance12 { get; set; }

        /// <summary>
        /// Gets or sets associated ledger account
        /// </summary>
        public virtual LedgerAccount LedgerAccount { get; set; }
    }
}