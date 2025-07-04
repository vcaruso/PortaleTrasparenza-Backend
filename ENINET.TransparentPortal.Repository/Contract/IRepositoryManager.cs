namespace ENINET.TransparentPortal.Repository.Contract;

public interface IRepositoryManager
{
    ISiteRepository Site { get; }

    IApplicationUserRepository ApplicationUser { get; }
    IApplicationGroupRepository ApplicationGroup { get; }
    ISitesUserRepository SitesUser { get; }
    IUserGroupRepository UserGroup { get; }
    IReportRepository Report { get; }
    IElementRepository Element { get; }

    IElementSiteRepository ElementSite { get; }
    int Save();
}
