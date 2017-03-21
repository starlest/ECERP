namespace ECERP.Business.Services
{
    using System;
    using Abstract;
    using Data.Abstract;
    using Models.Entities;

    public class SystemParameterService : ISystemParameterService
    {
        #region Private Members
        private readonly IRepository _repository;
        #endregion

        #region Constructor
        public SystemParameterService(IRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Interface Methods
        // TODO: Test case
        public SystemParameter GetSingleByKey(int companyId, string key)
        {
            return _repository.GetOne<SystemParameter>(p => p.CompanyId.Equals(companyId) && p.Key.Equals(key));
        }

        // TODO: Test case
        public void Create(int companyId, string key, string value, string createdBy)
        {
            var paramater = new SystemParameter
            {
                CompanyId = companyId,
                Key = key,
                Value = value
            };
            _repository.Create(paramater, createdBy);
            _repository.Save();
        }

        public void Update(int companyId, string key, string modifiedValue, string modifiedBy)
        {
            var parameter = _repository.GetOne<SystemParameter>(p => p.CompanyId.Equals(companyId) && p.Key.Equals(key));
            parameter.Value = modifiedValue;
            _repository.Update(parameter, modifiedBy);
            _repository.Save();
        }

        // TODO: Test case
        public DateTime GetLedgerCurrentPeriodStartDate(int companyId)
        {
            var parameter =
                _repository.GetOne<SystemParameter>(
                    p => p.CompanyId.Equals(companyId) && p.Key.Equals(Constants.LedgerCurrentPeriodStartDate));
            if (parameter == null)
                throw new Exception("LedgerCurrentPeriodStartDate paramater could not be found.");
            return DateTime.Parse(parameter.Value);
        }
        #endregion
    }
}