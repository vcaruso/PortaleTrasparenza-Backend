using ENINET.TransaprentPortal.Persistence.Entities;
using System.Linq.Expressions;
namespace ENINET.TransparentPortal.Repository.Contract;

public interface ISiteRepository
{
    IQueryable<Site> FindAll(bool trackChanges);
    IQueryable<Site> FindByCondition(Expression<Func<Site, object>>? includes, Expression<Func<Site, bool>> expression, bool trackChanges);
}
