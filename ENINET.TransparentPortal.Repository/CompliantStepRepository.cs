using ENINET.TransaprentPortal.Persistence;
using ENINET.TransparentPortal.Persistence.Entities;
using ENINET.TransparentPortal.Repository.Abstract;
using ENINET.TransparentPortal.Repository.Contract;

namespace ENINET.TransparentPortal.Repository
{
    public class CompliantStepRepository : RepositoryBase<ComplaintStep>, IComplaintStepRepository
    {
        public CompliantStepRepository(AppDbContext repositpryContext) : base(repositpryContext)
        {
        }
    }
}
