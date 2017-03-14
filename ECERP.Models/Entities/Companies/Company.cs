﻿namespace ECERP.Models.Entities.Companies
{
    using System.ComponentModel.DataAnnotations;
    using FinancialAccounting;

    public class Company : Entity<int>
    {
        #region Constructor
        public Company()
        {
        }
        #endregion

        #region Properties
        [Required, MaxLength(50)]
        public string Name { get; set; }
        #endregion

        #region Related Properties
        public virtual ChartOfAccounts ChartOfAccounts { get; set; }
        #endregion
    }
}