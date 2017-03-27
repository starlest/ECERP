namespace ECERP.Core.Domain.FinancialAccounting
{
    public static class LedgerAccountBalanceExtensions
    {
        /// <summary>
        /// Gets the given month balance
        /// </summary>
        /// <param name="ledgerAccountBalance">Ledger Account Balance</param>
        /// <param name="month">Month</param>
        /// <returns>Period Balance</returns>
        public static decimal GetMonthBalance(this LedgerAccountBalance ledgerAccountBalance, int month)
        {
            switch (month)
            {
                case 0:
                    return ledgerAccountBalance.BeginningBalance;
                case 1:
                    return ledgerAccountBalance.Balance1;
                case 2:
                    return ledgerAccountBalance.Balance2;
                case 3:
                    return ledgerAccountBalance.Balance3;
                case 4:
                    return ledgerAccountBalance.Balance4;
                case 5:
                    return ledgerAccountBalance.Balance5;
                case 6:
                    return ledgerAccountBalance.Balance6;
                case 7:
                    return ledgerAccountBalance.Balance7;
                case 8:
                    return ledgerAccountBalance.Balance8;
                case 9:
                    return ledgerAccountBalance.Balance9;
                case 10:
                    return ledgerAccountBalance.Balance10;
                case 11:
                    return ledgerAccountBalance.Balance11;
                case 12:
                    return ledgerAccountBalance.Balance12;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Sets the given month balance
        /// </summary>
        /// <param name="ledgerAccountBalance">Ledger Account Balance</param>
        /// <param name="month">Month</param>
        /// <param name="amount">Amount</param>
        /// <returns>Period Balance</returns>
        public static void SetMonthBalance(this LedgerAccountBalance ledgerAccountBalance, int month, decimal amount)
        {
            switch (month)
            {
                case 0:
                    ledgerAccountBalance.BeginningBalance = amount;
                    break;
                case 1:
                    ledgerAccountBalance.Balance1 = amount;
                    break;
                case 2:
                    ledgerAccountBalance.Balance2 = amount;
                    break;
                case 3:
                    ledgerAccountBalance.Balance3 = amount;
                    break;
                case 4:
                    ledgerAccountBalance.Balance4 = amount;
                    break;
                case 5:
                    ledgerAccountBalance.Balance5 = amount;
                    break;
                case 6:
                    ledgerAccountBalance.Balance6 = amount;
                    break;
                case 7:
                    ledgerAccountBalance.Balance7 = amount;
                    break;
                case 8:
                    ledgerAccountBalance.Balance8 = amount;
                    break;
                case 9:
                    ledgerAccountBalance.Balance9 = amount;
                    break;
                case 10:
                    ledgerAccountBalance.Balance10 = amount;
                    break;
                case 11:
                    ledgerAccountBalance.Balance11 = amount;
                    break;
                case 12:
                    ledgerAccountBalance.Balance12 = amount;
                    break;
            }
        }
    }
}