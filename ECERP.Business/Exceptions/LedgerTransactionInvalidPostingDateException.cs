using System;

namespace ECERP.Business.Exceptions
{
    public class LedgerTransactionInvalidPostingDateException : LedgerTransactionException
    {
        public LedgerTransactionInvalidPostingDateException()
        {
        }

        public LedgerTransactionInvalidPostingDateException(string message)
            : base(message)
        {
        }

        public LedgerTransactionInvalidPostingDateException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}