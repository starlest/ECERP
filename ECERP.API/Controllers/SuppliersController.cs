﻿namespace ECERP.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using AutoMapper;
    using Core;
    using Core.Domain;
    using Core.Domain.Suppliers;
    using Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Cities;
    using Services.Companies;
    using Services.Suppliers;
    using Services.SupplierSubscriptions;
    using ViewModels;

    public class SuppliersController : BaseController
    {
        #region Fields
        private readonly ICityService _cityService;
        private readonly ICompanyService _companyService;
        private readonly ISupplierSubscriptionService _supplierSubscriptionService;
        private readonly ISupplierService _supplierService;
        #endregion

        #region Constructor
        public SuppliersController(ECERPDbContext dbContext,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ICityService cityService,
            ICompanyService companyService,
            ISupplierSubscriptionService supplierSubscriptionService,
            ISupplierService supplierService) : base(dbContext, signInManager, userManager)
        {
            _cityService = cityService;
            _companyService = companyService;
            _supplierSubscriptionService = supplierSubscriptionService;
            _supplierService = supplierService;
        }
        #endregion

        #region RESTful Conventions
        /// <summary>
        /// GET: suppliers
        /// </summary>
        /// <returns>An array of all Json-serialized suppliers.</returns>
        [HttpGet]
        public IActionResult Get(
            [FromQuery] string nameFilter,
            [FromQuery] string addressFilter,
            [FromQuery] string cityFilter,
            [FromQuery] string contactNumberFilter,
            [FromQuery] string isActiveFilter,
            [FromQuery] string sortOrder,
            [FromQuery] int pageIndex,
            [FromQuery] int pageSize)
        {
            pageSize = pageSize == 0 ? int.MaxValue : pageSize;
            var filter = GenerateFilter(nameFilter, addressFilter, cityFilter, contactNumberFilter, isActiveFilter);
            var orderBy = GenerateSortOrder(sortOrder);
            var suppliers = _supplierService.GetSuppliers(filter, orderBy, pageIndex, pageSize);
            var suppliersVM = Mapper.Map<IPagedList<Supplier>, PagedListViewModel<SupplierViewModel>>(suppliers);
            foreach (var supplierVM in suppliersVM.Source)
            {
                supplierVM.Companies = GetSupplierCompanyNames(supplierVM.Id);
            }
            return new JsonResult(suppliersVM, DefaultJsonSettings);
        }

        /// <summary>
        ///     GET: suppliers/{id}
        /// </summary>
        /// <param name="id">Supplier Identifier</param>
        /// <returns>A Json-serialized object representing a single supplier.</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var supplier = _supplierService.GetSupplierById(id);
            if (supplier == null) return NotFound(new { Error = "not found" });
            var supplierVM = Mapper.Map<Supplier, SupplierViewModel>(supplier);
            supplierVM.Companies = GetSupplierCompanyNames(id);
            return new JsonResult(supplierVM, DefaultJsonSettings);
        }

        /// <summary>
        /// POST: suppliers
        /// </summary>
        /// <returns>Creates a new Supplier and return it accordingly.</returns>
        [HttpPost]
        public IActionResult Add([FromBody] SupplierViewModel svm)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var city = _cityService.GetCityByName(svm.City);
                if (city == null)
                    return NotFound(new { Error = "City could not be found." });

                var supplier = new Supplier
                {
                    Name = svm.Name,
                    Address = svm.Address,
                    ContactNumber = svm.ContactNumber,
                    CityId = city.Id,
                    TaxId = svm.TaxId
                };

                _supplierService.InsertSupplier(supplier);

                var supplierVM = Mapper.Map<Supplier, SupplierViewModel>(supplier);
                supplierVM.Companies = GetSupplierCompanyNames(supplierVM.Id);

                // return the newly-created supplier to the client.
                return new JsonResult(supplierVM, DefaultJsonSettings);
            }
            catch (Exception)
            {
                // return the error.
                return BadRequest(new { Error = "Check that all the fields are valid." });
            }
        }

        /// <summary>
        /// PUT: suppliers/{id}
        /// </summary>
        /// <returns>Updates an existing supplier and return it accordingly.</returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] SupplierViewModel svm)
        {
            if (svm == null) return NotFound(new { Error = "Supplier could not be found" });

            var supplier = _supplierService.GetSupplierById(id);

            if (supplier == null) return NotFound(new { Error = "Supplier could not be found" });

            var city = _cityService.GetCityByName(svm.City);
            if (city == null)
                return NotFound(new { Error = "City could not be found." });

            // handle the update (on per-property basis)
            supplier.Name = svm.Name;
            supplier.Address = svm.Address;
            supplier.CityId = city.Id;
            supplier.ContactNumber = svm.ContactNumber;
            supplier.TaxId = svm.TaxId;

            _supplierService.UpdateSupplier(supplier);

            supplier = _supplierService.GetSupplierById(id);

            var supplierVM = Mapper.Map<Supplier, SupplierViewModel>(supplier);
            supplierVM.Companies = GetSupplierCompanyNames(supplierVM.Id);

            return new JsonResult(supplierVM, DefaultJsonSettings);
        }

        /// <summary>
        /// POST: suppliers/{id}/subscribe
        /// </summary>
        /// <returns>Subscribe a supplier to a company and return it accordingly.</returns>
        [HttpPost("{id}/subscribe")]
        public IActionResult Subscribe(int id, [FromQuery] int companyId)
        {
            var supplier = _supplierService.GetSupplierById(id);
            if (supplier == null) return NotFound(new { Error = "Supplier could not be found" });

            var company = _companyService.GetCompanyById(companyId);
            if (company == null) return NotFound(new { Error = "Company could not be found" });

            var supplierSubscription = _supplierSubscriptionService.GetSupplierSubscription(id, companyId);
            if (supplierSubscription != null && supplierSubscription.IsActive)
                return BadRequest(new { Error = "Supplier is already subscribed to company." });

            _supplierSubscriptionService.Subscribe(id, companyId);

            supplier = _supplierService.GetSupplierById(id);

            var supplierVM = Mapper.Map<Supplier, SupplierViewModel>(supplier);
            supplierVM.Companies = GetSupplierCompanyNames(supplierVM.Id);

            return new JsonResult(supplierVM, DefaultJsonSettings);
        }

        /// <summary>
        /// POST: suppliers/{id}/unsubscribe
        /// </summary>
        /// <returns>Unsubscribe a supplier from a company and return it accordingly.</returns>
        [HttpPost("{id}/unsubscribe")]
        public IActionResult Unsubscribe(int id, [FromQuery] int companyId)
        {
            var supplierSubscription = _supplierSubscriptionService.GetSupplierSubscription(id, companyId);
            if (supplierSubscription == null) return BadRequest(new { Error = "Supplier is not subscribed to company." });

            _supplierSubscriptionService.Unsubscribe(id, companyId);

            var supplier = _supplierService.GetSupplierById(id);

            var supplierVM = Mapper.Map<Supplier, SupplierViewModel>(supplier);
            supplierVM.Companies = GetSupplierCompanyNames(supplierVM.Id);

            return new JsonResult(supplierVM, DefaultJsonSettings);
        }
        #endregion

        #region Utilities
        private static Expression<Func<Supplier, bool>> GenerateFilter(
            string nameFilter,
            string addressFilter,
            string cityFilter,
            string contactNumberFilter,
            string isActiveFilter)
        {
            var filter = PredicateBuilder.True<Supplier>();

            if (!string.IsNullOrEmpty(nameFilter))
                filter = filter.And(x => x.Name.IndexOf(nameFilter, 0, StringComparison.CurrentCultureIgnoreCase) != -1);

            if (!string.IsNullOrEmpty(addressFilter))
                filter =
                    filter.And(x => x.Address.IndexOf(addressFilter, 0, StringComparison.CurrentCultureIgnoreCase) != -1);

            if (!string.IsNullOrEmpty(cityFilter))
                filter =
                    filter.And(x => x.City.Name.IndexOf(cityFilter, 0, StringComparison.CurrentCultureIgnoreCase) != -1);

            if (!string.IsNullOrEmpty(contactNumberFilter))
                filter =
                    filter.And(
                        x =>
                            x.ContactNumber.IndexOf(contactNumberFilter, 0, StringComparison.CurrentCultureIgnoreCase) !=
                            -1);

            if (!string.IsNullOrEmpty(isActiveFilter))
                filter =
                    filter.And(
                        x =>
                            x.IsActive.ToString().IndexOf(isActiveFilter, 0, StringComparison.CurrentCultureIgnoreCase) !=
                            -1);

            return filter;
        }

        private static Func<IQueryable<Supplier>, IOrderedQueryable<Supplier>> GenerateSortOrder(
            string sortOrder)
        {
            switch (sortOrder)
            {
                case "name_asc":
                    return x => x.OrderBy(la => la.Name);
                case "name_desc":
                    return x => x.OrderByDescending(la => la.Name);
                case "address_asc":
                    return x => x.OrderBy(la => la.Address);
                case "address_desc":
                    return x => x.OrderByDescending(la => la.Address);
                case "city_asc":
                    return x => x.OrderBy(la => la.City);
                case "city_desc":
                    return x => x.OrderByDescending(la => la.City);
                default:
                    return null;
            }
        }

        private IList<string> GetSupplierCompanyNames(int supplierId)
        {
            return _supplierSubscriptionService.GetSupplierCompanies(supplierId).Select(c => c.Name).ToList();
        }
        #endregion
    }
}