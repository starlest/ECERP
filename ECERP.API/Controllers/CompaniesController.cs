namespace ECERP.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Core.Domain;
    using Core.Domain.Companies;
    using Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Companies;
    using ViewModels;

    public class CompaniesController : BaseController
    {
        #region Fields
        private readonly ICompanyService _companyService;
        #endregion

        #region Constructor
        public CompaniesController(ECERPDbContext dbContext,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ICompanyService companyService) : base(dbContext, signInManager, userManager)
        {
            _companyService = companyService;
        }
        #endregion

        #region RESTful Conventions
        /// <summary>
        /// GET: companies
        /// </summary>
        /// <returns>An array of all Json-serialized companies.</returns>
        [HttpGet]
        public IActionResult Get()
        {
            var companies = _companyService.GetAllCompanies();
            return new JsonResult(Mapper.Map<IList<Company>, IList<CompanyViewModel>>(companies), DefaultJsonSettings);
        }

        /// <summary>
        /// POST: companies
        /// </summary>
        /// <returns>Creates a new Company and return it accordingly.</returns>
        [HttpPost]
        public IActionResult Add([FromBody] CompanyViewModel cvm)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                // TODO: get the admin creating the company
                // get the admin creating the company
                //                var adminId = GetCurrentUserId();
                //                if (adminId == null) return NotFound(new { error = "User is not authenticated." });
                //                var admin = _dbContext.Admins.SingleOrDefault(i => i.Id == adminId);
                //                if (admin == null) return NotFound(new { error = $"User ID {adminId} has not been found" });

                // create a new company with the client-sent json data
                var company = new Company
                {
                    Name = cvm.Name
                };

                _companyService.InsertCompany(company);

                // return the newly-created company to the client.
                return new JsonResult(Mapper.Map<Company, CompanyViewModel>(company), DefaultJsonSettings);
            }
            catch (Exception)
            {
                // return the error.
                return BadRequest(new { Error = "Check that all the fields are valid." });
            }
        }
        #endregion
    }
}