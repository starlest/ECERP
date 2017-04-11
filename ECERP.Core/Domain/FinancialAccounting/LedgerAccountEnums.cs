namespace ECERP.Core.Domain.FinancialAccounting
{
    /// <summary>
    /// Ledger account types
    /// </summary>
    public enum LedgerAccountType
    {
        Asset = 1,
        Liability = 2,
        Equity = 3,
        Revenue = 4,
        Expense = 5,
        ContraAsset = -1,
        ContraLiability = -2,
        ContraEquity = -3,
        ContraRevenue = -4,
        ContraExpense = -5
    }

    /// <summary>
    /// Ledger account groups
    /// </summary>
    public enum LedgerAccountGroup
    {
        // Current Assets
        CashAndBank = 101,
        TemporaryInvestments = 102,
        AccountsReceivable = 103,
        Inventory = 104,
        Supplies = 105,
        PrepaidInsurance = 106,

        // PPE
        Land = 107,
        LandImprovements = 108,
        Buildings = 109,
        Equipment = 110,
        AccumulatedDepreciation = 111,

        // Intangible Assets
        Goodwill = 112,
        TradeNames = 113,

        // Other Assets
        OtherAssets = 114,

        // Current Liabilities
        ShortTermNotesPayable = 201,
        AccountsPayable = 202,
        WagesPayable = 203,
        InterestPayable = 204,
        TaxesPayable = 205,
        WarrantyLiability = 206,
        UnearnedRevenues = 207,

        // Long-term Liabilities
        LongTermNotesPayable = 208,
        BondsPayable = 209,

        // Equity
        CommonStock = 301,
        RetainedEarnings = 302,
        AccumulatedOtherComprehensiveIncome = 303,
        TreasuryStock = 304,

        // Operating Revenues
        SalesRevenue = 401,

        // Cost of Goods Sold
        CostOfGoodsSold = 502,

        // Operating Expenses
        SellingExpenses = 503,
        AdministrativeExpenses = 504,

        // Contra Revenues
        SalesReturnsAndAllowances = -401,

        // Contra Expenses
        PurchaseReturnsAndAllowances = -501,
    }
}