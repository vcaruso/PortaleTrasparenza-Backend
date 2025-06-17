using ENINET.TransaprentPortal.Persistence;
using ENINET.TransaprentPortal.Persistence.Entities;
using ENINET.TransparentPortal.Repository.Abstract;
using ENINET.TransparentPortal.Repository.Contract;

namespace ENINET.TransparentPortal.Repository;

public class ApplicationGroupRepository : RepositoryBase<ApplicationGroup>, IApplicationGroupRepository
{
    public ApplicationGroupRepository(AppDbContext context) : base(context)
    {

    }
}
