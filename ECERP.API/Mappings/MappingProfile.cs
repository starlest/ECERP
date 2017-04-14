namespace ECERP.API.Mappings
{
    using System;
    using System.Globalization;
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

            CreateMap<LedgerTransactionLine, LedgerTransactionLineViewModel>();
            CreateMap<LedgerTransactionLineViewModel, LedgerTransactionLine>();

            CreateMap<LedgerTransaction, LedgerTransactionViewModel>()
                .ForMember(ltvm => ltvm.CreatedDate, conf => conf.MapFrom(lt => lt.CreatedDate.ToString("dd-MM-yyyy")))
                .ForMember(ltvm => ltvm.PostingDate, conf => conf.MapFrom(lt => lt.PostingDate.ToString("dd-MM-yyyy")));

            CreateMap<LedgerTransactionViewModel, LedgerTransaction>()
                .ForMember(lt => lt.PostingDate, conf => conf.MapFrom(ltvm => DateTime.ParseExact(ltvm.PostingDate, "dd-MM-yyyy", CultureInfo.InvariantCulture)));
        }
    }
}