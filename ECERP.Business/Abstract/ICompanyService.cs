namespace ECERP.Business.Abstract
{
    using System.Collections.Generic;
    using Models.Entities.Companies;

    public interface ICompanyService
    {
        IEnumerable<Company> GetAll();
        Company GetSingleById(int id);
        Company GetSingleByName(string name);
        void CreateCompany(string name);
    }
}