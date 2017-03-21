namespace ECERP.Models.Entities
{
    using System.Collections.Generic;

    public abstract class Transaction : Entity<int>
    {
        #region Constructor
        protected Transaction()
        {
            TransactionLines = new List<TransactionLine>();
        }
        #endregion

        #region Related Properties
        public virtual List<TransactionLine> TransactionLines { get; set; }
        #endregion
    }
}