namespace ECERP.Business.Abstract
{
    using System;
    using Core.Domain;
    using Core.Domain.Configuration;
    using Models.Entities;

    public interface ISystemParameterService
    {
        CompanySetting GetSingleByKey(int companyId, string key);
        void Create(int companyId, string key, string value, string createdBy);
        void Update(int companyId, string key, string modifiedValue, string modifiedBy);
        DateTime GetLedgerCurrentPeriodStartDate(int companyId);
    }
}