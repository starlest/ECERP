namespace ECERP.API.Controllers
{
    using System;
    using System.Globalization;
    using System.Linq;
    using AutoMapper;
    using Core.Domain;
    using Core.Domain.FinancialAccounting;
    using Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.FinancialAccounting;
    using ViewModels;

    public class LedgerTransactionsController : BaseController
    {
        #region Fields
        private readonly ILedgerTransactionService _ledgerTransactionService;
        private readonly IChartOfAccountsService _chartOfAccountsService;
        private readonly ILedgerAccountService _ledgerAccountService;
        #endregion

        #region Constructor
        public LedgerTransactionsController(ECERPDbContext dbContext,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ILedgerTransactionService transactionService,
            IChartOfAccountsService chartOfAccountsService,
            ILedgerAccountService ledgerAccountService) : base(dbContext, signInManager, userManager)
        {
            _ledgerTransactionService = transactionService;
            _chartOfAccountsService = chartOfAccountsService;
            _ledgerAccountService = ledgerAccountService;
        }
        #endregion

        #region RESTful Conventions
        /// <summary>
        /// GET: ledgertransactions/{id}
        /// </summary>
        /// <param name="id">Ledger transaction identifier</param>
        /// <returns>A Json-serialized object representing a single ledger transactions.</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var ledgerTransaction = _ledgerTransactionService.GetLedgerTransactionById(id);
            if (ledgerTransaction == null) return NotFound(new { Error = "not found" });
            return new JsonResult(Mapper.Map<LedgerTransaction, LedgerTransactionViewModel>(ledgerTransaction),
                DefaultJsonSettings);
        }

        /// <summary>
        /// POST: ledgertransactions
        /// </summary>
        /// <returns>Creates a new Ledger Transaction and return it accordingly.</returns>
        [HttpPost]
        public IActionResult Add([FromBody] LedgerTransactionViewModel ltvm)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                // TODO: get the user creating the ledger account
                // get the admin creating the student
                //                var adminId = GetCurrentUserId();
                //                if (adminId == null) return NotFound(new { error = "User is not authenticated." });
                //                var admin = _dbContext.Admins.SingleOrDefault(i => i.Id == adminId);
                //                if (admin == null) return NotFound(new { error = $"User ID {adminId} has not been found" });

                var ledgerTransaction = new LedgerTransaction
                {
                    Documentation = ltvm.Documentation,
                    Description = ltvm.Description,
                    PostingDate = DateTime.ParseExact(ltvm.PostingDate, "dd-MM-yyyy", CultureInfo.InvariantCulture),
                    ChartOfAccountsId = ltvm.ChartOfAccountsId,
                    LedgerTransactionLines = ltvm.LedgerTransactionLines.Select(line => new LedgerTransactionLine
                    {
                        Amount = line.Amount,
                        IsDebit = line.IsDebit,
                        LedgerAccountId = line.LedgerAccountId
                    }).ToList()
                };

                _ledgerTransactionService.InsertLedgerTransaction(ledgerTransaction);

                // return the newly-created ledger transaction to the client.
                return new JsonResult(Mapper.Map<LedgerTransaction, LedgerTransactionViewModel>(ledgerTransaction),
                    DefaultJsonSettings);
            }
            catch (Exception e)
            {
                // return the error.
                return BadRequest(new { Error = e.Message });
            }
        }
        #endregion
    }
}