namespace ECERP.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Core.Domain;
    using Core.Domain.Cities;
    using Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Cities;
    using ViewModels;

    public class CitiesController : BaseController
    {
        #region Fields
        private readonly ICitiesService _citiesService;
        #endregion

        #region Constructor
        public CitiesController(ECERPDbContext dbContext,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ICitiesService citiesService) : base(dbContext, signInManager, userManager)
        {
            _citiesService = citiesService;
        }
        #endregion

        #region RESTful Conventions
        /// <summary>
        /// GET: cities
        /// </summary>
        /// <returns>An array of all Json-serialized cities.</returns>
        [HttpGet]
        public IActionResult Get()
        {
            var cities = _citiesService.GetAllCities();
            return new JsonResult(Mapper.Map<IList<City>, IList<CityViewModel>>(cities), DefaultJsonSettings);
        }

        /// <summary>
        /// POST: cities
        /// </summary>
        /// <returns>Creates a new City and return it accordingly.</returns>
        [HttpPost]
        public IActionResult Add([FromBody] CityViewModel cvm)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                // create a new city with the client-sent json data
                var city = new City
                {
                    Name = cvm.Name
                };

                _citiesService.InsertCity(city);

                // return the newly-created company to the client.
                return new JsonResult(Mapper.Map<City, CityViewModel>(city), DefaultJsonSettings);
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