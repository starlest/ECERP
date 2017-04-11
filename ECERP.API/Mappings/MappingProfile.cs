namespace ECERP.API.Mappings
{
    using System.Linq;
    using AutoMapper;
    using Core;
    using Core.Domain.Companies;
    using Core.Domain.FinancialAccounting;
    using ViewModels;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyViewModel>()
                .ForMember(cm => cm.CreatedDate, conf => conf.MapFrom(c => c.CreatedDate.ToString("dd-MM-yyyy")))
                .ForMember(cm => cm.ChartOfAccountsId, conf => conf.MapFrom(c => c.ChartOfAccounts.Id));

            CreateMap<LedgerAccount, LedgerAccountViewModel>()
                .ForMember(lavm => lavm.CreatedDate, conf => conf.MapFrom(la => la.CreatedDate.ToString("dd-MM-yyyy")))
                .ForMember(lavm => lavm.Company, conf => conf.MapFrom(la => la.ChartOfAccounts.Company.Name));

            CreateMap<IPagedList<LedgerAccount>, PagedListViewModel<LedgerAccountViewModel>>()
                .ForMember(lvm => lvm.Source, conf => conf.MapFrom(l => l.ToList()));
        }
    }
}