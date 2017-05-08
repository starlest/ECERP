namespace ECERP.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using Core.Domain;
    using Core.Domain.FinancialAccounting;
    using Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.FinancialAccounting;
    using ViewModels;

    public class ChartOfAccountsController : BaseController
    {
        #region Fields
        private readonly IChartOfAccountsService _chartOfAccountsService;
        #endregion

        #region Constructor
        public ChartOfAccountsController(ECERPDbContext dbContext,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IChartOfAccountsService chartOfAccountsService) : base(dbContext, signInManager, userManager)
        {
            _chartOfAccountsService = chartOfAccountsService;
        }
        #endregion

        #region RESTful Conventions
        /// <summary>
        /// GET: id/balancesheet/
        /// </summary>
        /// <param name="id">Chart of Accounts Identifier</param>
        /// <param name="year">Period Year</param>
        /// <param name="month">Period Month</param>
        /// <returns>Balance sheet for the given period</returns>
        [HttpGet("{id}/balancesheet")]
        public IActionResult GetBalanceSheet(int id, [FromQuery] int year, [FromQuery] int month)
        {
            var balanceSheet = _chartOfAccountsService.GetBalanceSheet(id, year, month);
            return new JsonResult(AutoMapper.Mapper.Map<IList<LedgerBalanceSheetItem>, IList<LedgerBalanceSheetItemViewModel>>(balanceSheet), DefaultJsonSettings);
        }

        /// <summary>
        /// GET: id/currentperiod/
        /// </summary>
        /// <param name="id">Chart of Accounts Identifier</param>
        /// <returns>Start date of current period</returns>
        [HttpGet("{id}/currentperiod")]
        public IActionResult GetCurrentPeriod(int id)
        {
            var coa = _chartOfAccountsService.GetChartOfAccountsById(id);
            return new JsonResult(coa.CurrentLedgerPeriodStartDate.ToString("dd-MM-yyyy"));
        }

        /// <summary>
        /// POST: id/closeperiod/
        /// </summary>
        /// <param name="id">Chart of Accounts Identifier</param>
        /// <returns></returns>
        [HttpPost("{id}/closeperiod")]
        public IActionResult CloseCurrentPeriod(int id)
        {
            try
            {
                _chartOfAccountsService.CloseLedgerPeriod(id);
                var coa = _chartOfAccountsService.GetChartOfAccountsById(id);
                return new JsonResult(coa.CurrentLedgerPeriodStartDate.ToString("dd-MM-yyyy"));
            }
            catch (Exception)
            {
                // return the error.
                return BadRequest(new { Error = "An error occured while closing period." });
            }
        }

        /// <summary>
        /// POST: id/regressperiod/
        /// </summary>
        /// <param name="id">Chart of Accounts Identifier</param>
        /// <returns></returns>
        [HttpPost("{id}/regressperiod")]
        public IActionResult RegressPeriod(int id)
        {
            try
            {
                _chartOfAccountsService.RegressLedgerPeriod(id);
                var coa = _chartOfAccountsService.GetChartOfAccountsById(id);
                return new JsonResult(coa.CurrentLedgerPeriodStartDate.ToString("dd-MM-yyyy"));
            }
            catch (Exception)
            {
                // return the error.
                return BadRequest(new { Error = "An error occured while regressing period." });
            }
        }
        #endregion
    }
}