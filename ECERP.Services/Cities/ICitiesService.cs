namespace ECERP.Services.Cities
{
    using System.Collections.Generic;
    using Core.Domain.Cities;

    public interface ICitiesService
    {
        /// <summary>
        /// Gets all cities
        /// </summary>
        /// <returns>Cities</returns>
        IList<City> GetAllCities();

        /// <summary>
        /// Gets a city by identifier
        /// </summary>
        /// <param name="id">City Identifier</param>
        /// <returns>City</returns>
        City GetCityById(int id);

        /// <summary>
        /// Gets a city by name
        /// </summary>
        /// <param name="name">City Name</param>
        /// <returns>A City</returns>
        City GetCityByName(string name);

        /// <summary>
        /// Insert a city
        /// </summary>
        /// <param name="city">City</param>
        void InsertCity(City city);
    }
}