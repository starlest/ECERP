namespace ECERP.API.Controllers
{
    using System;
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
        #endregion

        #region Constructor
        public LedgerTransactionsController(ECERPDbContext dbContext,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ILedgerTransactionService transactionService) : base(dbContext, signInManager, userManager)
        {
            _ledgerTransactionService = transactionService;
        }
        #endregion

        #region RESTful Conventions
        /// <summary>
        /// GET: ledgertransactions/{id}
        /// </summary>
        /// <param name="id">Ledger transactions identifier</param>
        /// <returns>A Json-serialized object representing a single ledger transactions.</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var ledgerTransaction = _ledgerTransactionService.GetLedgerTransactionById(id);
            if (ledgerTransaction == null) return NotFound(new { Error = "not found" });
            return new JsonResult(Mapper.Map<LedgerTransaction, LedgerTransaction>(ledgerTransaction),
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

                var ledgerTransaction = Mapper.Map<LedgerTransactionViewModel, LedgerTransaction>(ltvm);

//                _ledgerTransactionService.InsertLedgerTransaction(ledgerTransaction);

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