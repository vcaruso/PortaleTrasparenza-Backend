using ENINET.TransaprentPortal.Persistence;
using ENINET.TransparentPortal.Persistence.Entities;
using ENINET.TransparentPortal.Repository.Abstract;
using ENINET.TransparentPortal.Repository.Contract;

namespace ENINET.TransparentPortal.Repository
{
    public class GuestAuthRepository : RepositoryBase<GuestAuth>, IGuestAuthRepository
    {
        public GuestAuthRepository(AppDbContext context) : base(context)
        {
        }
    }
}
