namespace ECERP.Services.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core;
    using Core.Domain.Configuration;
    using Data.Abstract;

    public class CompanySettingService : ICompanySettingService
    {
        #region Fields
        private readonly IRepository _repository;
        #endregion

        #region Constructor
        public CompanySettingService(IRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Get all company's settings
        /// </summary>
        /// <param name="companyId">Company identifier</param>
        /// <returns>Company Settings</returns>
        public virtual IList<CompanySetting> GetCompanySettings(int companyId)
        {
            return _repository.Get<CompanySetting>(x => x.CompanyId == companyId).ToList();
        }

        /// <summary>
        /// Get company setting by key
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="companyId">Company identifier</param>
        /// <returns>Company Setting</returns>
        public virtual CompanySetting GetCompanySettingByKey(string key, int companyId)
        {
            key = key.Trim().ToLowerInvariant();
            return _repository.GetOne<CompanySetting>(x => x.Key == key && x.CompanyId == companyId);
        }

        /// <summary>
        /// Get company setting value by key
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="companyId">Company identifier</param>
        /// <returns>Company Setting value</returns>
        public virtual T GetCompanySettingValueByKey<T>(string key, int companyId)
        {
            key = key.Trim().ToLowerInvariant();
            var companySetting = GetCompanySettingByKey(key, companyId);
            if (companySetting == null) return default(T);
            return CommonHelper.To<T>(companySetting.Value);
        }

        /// <summary>
        /// Set company setting value
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="companyId">Company identifier</param>
        public virtual void SetCompanySetting<T>(string key, T value, int companyId)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            key = key.Trim().ToLowerInvariant();
            var valueStr = CommonHelper.To<string>(value);
            var companySetting = GetCompanySettingByKey(key, companyId);

            // Update
            if (companySetting != null)
            {
                companySetting.Value = valueStr;
                _repository.Update(companySetting);
            }
            // Setting
            else
            {
                companySetting = new CompanySetting
                {
                    Key = key,
                    Value = valueStr,
                    CompanyId = companyId
                };
                _repository.Create(companySetting);
            }

            _repository.Save();
        }

        /// <summary>
        /// Deletes a company setting
        /// </summary>
        /// <param name="companySetting">Company Setting</param>
        public void DeleteCompanySetting(CompanySetting companySetting)
        {
            if (companySetting == null)
                throw new ArgumentNullException(nameof(companySetting));
            _repository.Delete(companySetting);
            _repository.Save();
        }
        #endregion
    }
}