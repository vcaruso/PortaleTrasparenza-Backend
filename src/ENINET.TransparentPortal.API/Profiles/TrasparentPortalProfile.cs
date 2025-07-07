using AnalisiHubApi.Dtos.App;
using AutoMapper;
using ENINET.TransaprentPortal.Persistence.Entities;
using ENINET.TransparentPortal.API.Dtos.App;
using ENINET.TransparentPortal.Persistence.Entities;

namespace ENINET.TransparentPortal.API.Profiles;

public class TrasparentPortalProfile : Profile
{
    public TrasparentPortalProfile()
    {

        CreateMap<ReportDto, Report>().ReverseMap();
        CreateMap<ElementDto, ElementSite>().ReverseMap();
        CreateMap<SiteDto, Site>().ReverseMap();

    }
}

