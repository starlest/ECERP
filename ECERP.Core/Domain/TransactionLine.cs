namespace ECERP.Models.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Core;
    using FinancialAccounting;

    public abstract class TransactionLine : Entity<int>
    {
        [Required, ForeignKey("Transaction")]
        public int TransactionId { get; set; }

        #region Related Properties
        public virtual Transaction Transaction { get; set; }
        #endregion
    }
}