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

    ICompliantOperationRepository CompliantOperation { get; }
    ICompliantRepository Compliant { get; }
    IComplaintStepRepository CompliantStep { get; }

    int Save();
}
