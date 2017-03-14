namespace ECERP.Models.Entities
{
    using System;

    public class Transaction
    {
        public int Id { get; set; }
        public string TransactionNumber { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
