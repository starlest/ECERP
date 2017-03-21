namespace ECERP.Business.Abstract
{
    using System;
    using Models.Entities;

    public interface ISystemParameterService
    {
        SystemParameter GetSingleByKey(int companyId, string key);
        void Create(int companyId, string key, string value, string createdBy);
        void Update(int companyId, string key, string modifiedValue, string modifiedBy);
        DateTime GetLedgerCurrentPeriodStartDate(int companyId);
    }
}