namespace ECERP.Business.Exceptions
{
    using System;

    public class LedgerTransactionException : Exception
    {
        public LedgerTransactionException()
        {
        }

        public LedgerTransactionException(string message)
            : base(message)
        {
        }

        public LedgerTransactionException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}