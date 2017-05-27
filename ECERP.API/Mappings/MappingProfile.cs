namespace ECERP.API.Mappings
{
    using System.Linq;
    using AutoMapper;
    using Core;
    using Core.Domain.Cities;
    using Core.Domain.Companies;
    using Core.Domain.FinancialAccounting;
    using Core.Domain.Suppliers;
    using ViewModels;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LedgerBalanceSheetItem, LedgerBalanceSheetItemViewModel>();

            CreateMap<City, CityViewModel>();

            CreateMap<Company, CompanyViewModel>()
                .ForMember(cm => cm.CreatedDate, conf => conf.MapFrom(c => c.CreatedDate.ToString("dd-MM-yyyy")))
                .ForMember(cm => cm.ChartOfAccountsId, conf => conf.MapFrom(c => c.ChartOfAccounts.Id));

            CreateMap<Supplier, SupplierViewModel>()
                .ForMember(svm => svm.City, conf => conf.MapFrom(s => s.City));

            CreateMap<IPagedList<Supplier>, PagedListViewModel<SupplierViewModel>>()
                .ForMember(svm => svm.Source, conf => conf.MapFrom(s => s.ToList()));

            CreateMap<LedgerAccount, LedgerAccountViewModel>()
                .ForMember(lavm => lavm.CreatedDate, conf => conf.MapFrom(la => la.CreatedDate.ToString("dd-MM-yyyy")))
                .ForMember(lavm => lavm.Company, conf => conf.MapFrom(la => la.ChartOfAccounts.Company.Name));

            CreateMap<IPagedList<LedgerAccount>, PagedListViewModel<LedgerAccountViewModel>>()
                .ForMember(lvm => lvm.Source, conf => conf.MapFrom(l => l.ToList()));

            CreateMap<IPagedList<LedgerTransaction>, PagedListViewModel<LedgerTransactionViewModel>>()
                .ForMember(lvm => lvm.Source, conf => conf.MapFrom(l => l.ToList()));

            CreateMap<LedgerTransactionLine, LedgerTransactionLineViewModel>();

            CreateMap<LedgerTransaction, LedgerTransactionViewModel>()
                .ForMember(ltvm => ltvm.CreatedDate, conf => conf.MapFrom(lt => lt.CreatedDate.ToString("dd-MM-yyyy")))
                .ForMember(ltvm => ltvm.PostingDate, conf => conf.MapFrom(lt => lt.PostingDate.ToString("dd-MM-yyyy")));
        }
    }
}