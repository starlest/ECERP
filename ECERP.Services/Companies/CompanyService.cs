namespace ECERP.Services.Companies
{
    using System.Collections.Generic;
    using Core;
    using Core.Domain.Companies;
    using Data.Abstract;

    public class CompanyService : ICompanyService
    {
        #region Fields
        private readonly IRepository _repository;
        #endregion

        #region Constructor
        public CompanyService(IRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets all companies
        /// </summary>
        /// <returns>Companies</returns>
        public IList<Company> GetAllCompanies()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets a company by identifier
        /// </summary>
        /// <param name="id">Company identifier</param>
        /// <returns>A company</returns>
        public virtual Company GetCompanyById(int id)
        {
            return _repository.GetById<Company>(id);
        }

        /// <summary>
        /// Gets a company by name
        /// </summary>
        /// <param name="name">Company name</param>
        /// <returns>A company</returns>
        public virtual Company GetCompanyByName(string name)
        {
            return _repository.GetOne<Company>(x => x.Name == name);
        }
        #endregion
    }
}