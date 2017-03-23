namespace ECERP.Models.Entities.FinancialAccounting
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class LedgerTransaction : Transaction
    {
        private DateTime? postingDate;

        [Required, MaxLength(50)]
        public string Documentation { get; set; }

        [Required, MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime PostingDate
        {
            get { return postingDate ?? DateTime.Now; }
            set { postingDate = value; }
        }
    }
}