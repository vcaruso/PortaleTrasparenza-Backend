using ENINET.TransaprentPortal.Persistence;
using ENINET.TransparentPortal.Persistence.Entities;
using ENINET.TransparentPortal.Repository.Abstract;
using ENINET.TransparentPortal.Repository.Contract;

namespace ENINET.TransparentPortal.Repository;

public class ComplaintRepository : RepositoryBase<Complaint>, ICompliantRepository
{
    public ComplaintRepository(AppDbContext repositpryContext) : base(repositpryContext)
    {
    }
}
