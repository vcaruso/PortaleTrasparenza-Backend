using AutoMapper;
using ENINET.TransaprentPortal.Persistence.Entities;
using ENINET.TransparentPortal.API.Dtos.App;

namespace ENINET.TransparentPortal.API.Profiles;

public class TrasparentPortalProfile : Profile
{
    public TrasparentPortalProfile()
    {

        CreateMap<ReportDto, Report>().ReverseMap();
        CreateMap<ElementDto, Element>().ReverseMap();

    }
}

