using AutoMapper;
using HepsiTools.Entities;
using HepsiTools.Helper;
using HepsiTools.Models;

namespace HepsiTools.AutoMapper
{
    public class ToolsProfile : Profile
    {
        public ToolsProfile()
        {
            CreateMap<WooCommercenModel, WooCommerceData>();
            CreateMap<WooCommerceData, WooCommercenModel>();

            CreateMap<WooCommerceInsertModel, WooCommerceData>();
            CreateMap<WooCommerceData, WooCommerceInsertModel>();

            CreateMap<CompanyInsertModel, Company>();
            CreateMap<Company, CompanyInsertModel>();


            CreateMap<CompanyModel, Company>();
            CreateMap<Company, CompanyModel>();

            CreateMap<CompetitionAnalysesInsertModel, CompetitionAnalyses>().ForMember(m => m.StatusType, opt => opt.MapFrom(x => (StatusType)x.StatusType));
            CreateMap<CompetitionAnalyses, CompetitionAnalysesInsertModel>().ForMember(m => m.StatusType, opt => opt.MapFrom(x => (int)x.StatusType)); ;

            CreateMap<CompetitionAnalysesModel, CompetitionAnalyses>().ForMember(m => m.StatusType, opt => opt.MapFrom(x => (StatusType)x.StatusType));
            CreateMap<CompetitionAnalyses, CompetitionAnalysesModel>().ForMember(m => m.StatusType, opt => opt.MapFrom(x => (int)x.StatusType)); ;
        }
    }

}
