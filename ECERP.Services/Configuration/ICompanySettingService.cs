namespace ECERP.Services.Configuration
{
    using System.Collections.Generic;
    using Core.Domain.Configuration;

    public interface ICompanySettingService
    {
        /// <summary>
        /// Get all company's settings
        /// </summary>
        /// <param name="companyId">Company identifier</param>
        /// <returns>Company Settings</returns>
        IList<CompanySetting> GetCompanySettings(int companyId);

        /// <summary>
        /// Get company setting by key
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="companyId">Company identifier</param>
        /// <returns>Company Setting</returns>
        CompanySetting GetCompanySettingByKey(string key, int companyId);

        /// <summary>
        /// Get company setting value by key
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="companyId">Company identifier</param>
        /// <returns>Company Setting value</returns>
        T GetCompanySettingValueByKey<T>(string key, int companyId);

        /// <summary>
        /// Set company setting value
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="companyId">Company identifier</param>
        void SetCompanySetting<T>(string key, T value, int companyId);


        /// <summary>
        /// Deletes a company setting
        /// </summary>
        /// <param name="companySetting">Company Setting</param>
        void DeleteCompanySetting(CompanySetting companySetting);
    }
}