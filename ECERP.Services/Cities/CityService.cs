namespace ECERP.Services.Cities
{
    using System.Collections.Generic;
    using System.Linq;
    using Core.Domain.Cities;
    using Data.Abstract;

    public class CityService : ICityService
    {
        #region Fields
        private readonly IRepository _repository;
        #endregion

        #region Constructor
        public CityService(IRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets all cities
        /// </summary>
        /// <returns>Cities</returns>
        public virtual IList<City> GetAllCities()
        {
            return _repository.GetAll<City>().ToList();
        }

        /// <summary>
        /// Gets a city by identifier
        /// </summary>
        /// <param name="id">City Identifier</param>
        /// <returns>City</returns>
        public virtual City GetCityById(int id)
        {
            return _repository.GetById<City>(id);
        }

        /// <summary>
        /// Gets a city by name
        /// </summary>
        /// <param name="name">City Name</param>
        /// <returns>City</returns>
        public virtual City GetCityByName(string name)
        {
            return _repository.GetOne<City>(c => c.Name.Equals(name));
        }

        /// <summary>
        /// Insert a city
        /// </summary>
        /// <param name="city">City</param>
        public void InsertCity(City city)
        {
            _repository.Create(city);
            _repository.Save();
        }
        #endregion
    }
}