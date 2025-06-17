
using ENINET.TransaprentPortal.Persistence.Entities;
using System.Linq.Expressions;

namespace ENINET.TransparentPortal.Repository.Contract;

public interface IUserGroupRepository
{
    bool Any(Expression<Func<UserGroup, bool>> expression);
    void Create(UserGroup favouriteProduct);
    IQueryable<UserGroup> FindByCondition(Expression<Func<UserGroup, object>>? includes, Expression<Func<UserGroup, bool>> expression, bool trackChanges);
    void Delete(UserGroup favouriteProduct);
}
