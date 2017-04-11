namespace ECERP.API.Controllers
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using AutoMapper;
    using Core;
    using Core.Domain;
    using Core.Domain.FinancialAccounting;
    using Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.FinancialAccounting;
    using ViewModels;

    public class LedgerAccountsController : BaseController
    {
        #region Fields
        private readonly ILedgerAccountService _ledgerAccountService;
        #endregion

        #region Constructor
        public LedgerAccountsController(ECERPDbContext dbContext,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ILedgerAccountService ledgerAccountService) : base(dbContext, signInManager, userManager)
        {
            _ledgerAccountService = ledgerAccountService;
        }
        #endregion

        #region RESTful Conventions
        /// <summary>
        /// GET: ledgeraccounts
        /// </summary>
        /// <returns>An array of all Json-serialized ledger accounts.</returns>
        [HttpGet]
        public IActionResult Get(
            [FromQuery] string accountNumberFilter,
            [FromQuery] string nameFilter,
            [FromQuery] string typeFilter,
            [FromQuery] string groupFilter,
            [FromQuery] string companyFilter,
            [FromQuery] string isActiveFilter,
            [FromQuery] string sortOrder,
            [FromQuery] int pageIndex,
            [FromQuery] int pageSize)
        {
            pageSize = pageSize == 0 ? int.MaxValue : pageSize;
            var filter = GenerateFilter(accountNumberFilter, nameFilter, typeFilter, groupFilter, companyFilter,
                isActiveFilter);
            var orderBy = GenerateSortOrder(sortOrder);
            var ledgerAccounts = _ledgerAccountService.GetLedgerAccounts(filter, orderBy, pageIndex, pageSize);
            return
                new JsonResult(
                    Mapper.Map<IPagedList<LedgerAccount>, PagedListViewModel<LedgerAccountViewModel>>(ledgerAccounts),
                    DefaultJsonSettings);
        }

        /// <summary>
        ///     GET: ledgeraccounts/{id}
        /// </summary>
        /// <param name="id">Ledger account identifier</param>
        /// <returns>A Json-serialized object representing a single ledger account.</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var ledgerAccount = _ledgerAccountService.GetLedgerAccountById(id);
            if (ledgerAccount == null) return NotFound(new { Error = "not found" });
            return new JsonResult(Mapper.Map<LedgerAccount, LedgerAccountViewModel>(ledgerAccount), DefaultJsonSettings);
        }

        /// <summary>
        /// POST: ledgeraccounts
        /// </summary>
        /// <returns>Creates a new Ledger Account and return it accordingly.</returns>
        [HttpPost]
        public IActionResult Add([FromBody] LedgerAccountViewModel lavm)
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

                var group = (LedgerAccountGroup) Enum.Parse(typeof(LedgerAccountGroup), lavm.Group);
                var type = (LedgerAccountType) CommonHelper.GetFirstDigit((int) group);

                var ledgerAccount = new LedgerAccount
                {
                    Name = lavm.Name,
                    Description = lavm.Description,
                    Type = type,
                    Group = group,
                    ChartOfAccountsId = lavm.ChartOfAccountsId
                };

                _ledgerAccountService.InsertLedgerAccount(ledgerAccount);

                // return the newly-created ledger account to the client.
                return new JsonResult(Mapper.Map<LedgerAccount, LedgerAccountViewModel>(ledgerAccount),
                    DefaultJsonSettings);
            }
            catch (Exception)
            {
                // return the error.
                return BadRequest(new { Error = "Check that all the fields are valid." });
            }
        }
        #endregion

        #region Utilities
        private static Expression<Func<LedgerAccount, bool>> GenerateFilter(
            string accountNumberFilter,
            string nameFilter,
            string typeFilter,
            string groupFilter,
            string companyFilter,
            string isActiveFilter)
        {
            var filter = PredicateBuilder.True<LedgerAccount>();

            if (!string.IsNullOrEmpty(accountNumberFilter))
                filter =
                    filter.And(
                        x =>
                            x.AccountNumber.ToString()
                                .IndexOf(accountNumberFilter, 0, StringComparison.CurrentCultureIgnoreCase) != -1);

            if (!string.IsNullOrEmpty(nameFilter))
                filter = filter.And(x => x.Name.IndexOf(nameFilter, 0, StringComparison.CurrentCultureIgnoreCase) != -1);

            if (!string.IsNullOrEmpty(typeFilter))
                filter =
                    filter.And(
                        x =>
                            Enum.GetName(typeof(LedgerAccountType), x.Type)
                                .IndexOf(typeFilter, 0, StringComparison.CurrentCultureIgnoreCase) != -1);

            if (!string.IsNullOrEmpty(groupFilter))
                filter =
                    filter.And(
                        x =>
                            Enum.GetName(typeof(LedgerAccountGroup), x.Group)
                                .IndexOf(groupFilter, 0, StringComparison.CurrentCultureIgnoreCase) != -1);

            if (!string.IsNullOrEmpty(companyFilter))
                filter =
                    filter.And(
                        x =>
                            x.ChartOfAccounts.Company.Name.IndexOf(companyFilter, 0,
                                StringComparison.CurrentCultureIgnoreCase) != -1);

            if (!string.IsNullOrEmpty(isActiveFilter))
                filter =
                    filter.And(
                        x =>
                            x.IsActive.ToString().IndexOf(isActiveFilter, 0, StringComparison.CurrentCultureIgnoreCase) !=
                            -1);

            return filter;
        }

        private static Func<IQueryable<LedgerAccount>, IOrderedQueryable<LedgerAccount>> GenerateSortOrder(
            string sortOrder)
        {
            switch (sortOrder)
            {
                case "name_asc":
                    return x => x.OrderBy(la => la.Name);
                case "name_desc":
                    return x => x.OrderByDescending(la => la.Name);
                case "accountnumber_asc":
                    return x => x.OrderBy(la => la.AccountNumber);
                case "accountnumber_desc":
                    return x => x.OrderByDescending(la => la.AccountNumber);
                case "type_asc":
                    return x => x.OrderBy(la => la.Type);
                case "type_desc":
                    return x => x.OrderByDescending(la => la.Type);
                case "group_asc":
                    return x => x.OrderBy(la => la.Group);
                case "group_desc":
                    return x => x.OrderByDescending(la => la.Group);
                case "company_asc":
                    return x => x.OrderBy(la => la.ChartOfAccounts.Company.Name);
                case "company_desc":
                    return x => x.OrderByDescending(la => la.ChartOfAccounts.Company.Name);
                default:
                    return null;
            }
        }
        #endregion
    }
}