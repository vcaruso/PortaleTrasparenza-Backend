

using ENINET.TransaprentPortal.Persistence;
using ENINET.TransparentPortal.Repository.Contract;

namespace ENINET.TransparentPortal.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly AppDbContext _context;
        ISiteRepository _sitesRepository = default!;

        IApplicationUserRepository _applicationUserRepository = default!;
        IApplicationGroupRepository _applicationGroupRepository = default!;
        ISitesUserRepository _raffinerieUserRepository = default!;
        IUserGroupRepository _userGroupRepository = default!;
        IReportRepository _reportRepository = default!;
        IElementRepository _elementRepository = default!;
        IElementSiteRepository _elementSiteRepository = default!;


        public RepositoryManager(AppDbContext context)
        {
            _context = context;
        }
        public ISiteRepository Site
        {
            get
            {
                if (_sitesRepository == null)
                {
                    _sitesRepository = new SiteRepository(_context);
                }
                return _sitesRepository;
            }
        }


        public IElementSiteRepository ElementSite
        {
            get
            {
                if (_elementSiteRepository == null)
                {
                    _elementSiteRepository = new ElementsSiteRepository(_context);
                }
                return (_elementSiteRepository);
            }
        }

        public IApplicationUserRepository ApplicationUser
        {
            get
            {
                if (_applicationUserRepository == null)
                {
                    _applicationUserRepository = new ApplicationUserRepository(_context);
                }
                return _applicationUserRepository;

            }
        }
        public IApplicationGroupRepository ApplicationGroup
        {
            get
            {
                if (_applicationGroupRepository == null)
                {
                    _applicationGroupRepository = new ApplicationGroupRepository(_context);
                }
                return _applicationGroupRepository;

            }
        }

        public ISitesUserRepository SitesUser
        {
            get
            {
                if (_raffinerieUserRepository == null)
                {
                    _raffinerieUserRepository = new SitesUserRepository(_context);
                }
                return _raffinerieUserRepository;

            }
        }

        public IUserGroupRepository UserGroup
        {
            get
            {
                if (_userGroupRepository == null)
                {
                    _userGroupRepository = new UserGroupRepository(_context);
                }
                return _userGroupRepository;

            }
        }

        public IReportRepository Report
        {
            get
            {
                if (_reportRepository == null)
                {
                    _reportRepository = new ReportRepository(_context);
                }
                return _reportRepository;

            }
        }

        public IElementRepository Element
        {
            get
            {
                if (_elementRepository == null)
                {
                    _elementRepository = new ElementRepository(_context);
                }
                return _elementRepository;

            }
        }



        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
