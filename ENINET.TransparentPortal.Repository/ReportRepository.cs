using ENINET.TransaprentPortal.Persistence;
using ENINET.TransaprentPortal.Persistence.Entities;
using ENINET.TransparentPortal.Repository.Abstract;
using ENINET.TransparentPortal.Repository.Contract;

namespace ENINET.TransparentPortal.Repository
{
    public class ReportRepository : RepositoryBase<Report>, IReportRepository
    {
        public ReportRepository(AppDbContext context) : base(context)
        {

        }
    }
}
