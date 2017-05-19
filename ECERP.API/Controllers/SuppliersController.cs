namespace ECERP.API.Controllers
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using AutoMapper;
    using Core;
    using Core.Domain;
    using Core.Domain.Suppliers;
    using Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Suppliers;
    using ViewModels;

    public class SuppliersController : BaseController
    {
        #region Fields
        private readonly ISuppliersService _suppliersService;
        #endregion

        #region Constructor
        public SuppliersController(ECERPDbContext dbContext,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ISuppliersService suppliersService) : base(dbContext, signInManager, userManager)
        {
            _suppliersService = suppliersService;
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
            var suppliers = _suppliersService.GetSuppliers(filter, orderBy, pageIndex, pageSize);
            return
                new JsonResult(
                    Mapper.Map<IPagedList<Supplier>, PagedListViewModel<SupplierViewModel>>(suppliers),
                    DefaultJsonSettings);
        }

        /// <summary>
        ///     GET: suppliers/{id}
        /// </summary>
        /// <param name="id">Supplier Identifier</param>
        /// <returns>A Json-serialized object representing a single supplier.</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var supplier = _suppliersService.GetSupplierById(id);
            if (supplier == null) return NotFound(new { Error = "not found" });
            return new JsonResult(Mapper.Map<Supplier, SupplierViewModel>(supplier), DefaultJsonSettings);
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
                var supplier = new Supplier
                {
                    Name = svm.Name,
                    Address = svm.Address,
                    ContactNumber = svm.ContactNumber,
                    CityId = svm.City.Id
                };

                _suppliersService.InsertSupplier(supplier);

                // return the newly-created supplier to the client.
                return new JsonResult(Mapper.Map<Supplier, SupplierViewModel>(supplier), DefaultJsonSettings);
            }
            catch (Exception)
            {
                // return the error.
                return BadRequest(new { Error = "Check that all the fields are valid." });
            }
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
        #endregion
    }
}