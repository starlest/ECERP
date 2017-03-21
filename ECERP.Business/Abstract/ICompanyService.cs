namespace ECERP.Business.Abstract
{
    using System.Collections.Generic;
    using Models.Entities.Companies;

    public interface ICompanyService
    {
        IEnumerable<Company> GetAll();
        Company GetSingleById(object id);
        Company GetSingleByName(string name);
        void Create(string name, string createdBy);
        void OpenLastLedgerPeriod(int companyId, string modifiedBy);
        void CloseCurrentLedgerPeriod(int companyId, string modifiedBy);
    }
}