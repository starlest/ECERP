namespace ECERP.Services.Companies
{
    using System.Collections.Generic;
    using Core.Domain.Companies;

    public interface ICompanyService
    {
        /// <summary>
        /// Gets all companies
        /// </summary>
        /// <returns>Companies</returns>
        IList<Company> GetAllCompanies();

        /// <summary>
        /// Gets a company by identifier
        /// </summary>
        /// <param name="id">Company identifier</param>
        /// <returns>A company</returns>
        Company GetCompanyById(int id);

        /// <summary>
        /// Gets a company by name
        /// </summary>
        /// <param name="name">Company name</param>
        /// <returns>A company</returns>
        Company GetCompanyByName(string name);
    }
}