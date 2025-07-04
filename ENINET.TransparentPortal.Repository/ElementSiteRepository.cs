using ENINET.TransaprentPortal.Persistence;
using ENINET.TransparentPortal.Persistence.Entities;
using ENINET.TransparentPortal.Repository.Abstract;
using ENINET.TransparentPortal.Repository.Contract;

namespace ENINET.TransparentPortal.Repository
{
    public class ElementsSiteRepository : RepositoryBase<ElementSite>, IElementSiteRepository
    {
        public ElementsSiteRepository(AppDbContext context) : base(context)
        {

        }
    }
}
