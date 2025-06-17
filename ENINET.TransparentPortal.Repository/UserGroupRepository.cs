using ENINET.TransaprentPortal.Persistence;
using ENINET.TransaprentPortal.Persistence.Entities;
using ENINET.TransparentPortal.Repository.Abstract;
using ENINET.TransparentPortal.Repository.Contract;

namespace ENINET.TransparentPortal.Repository;

public class UserGroupRepository : RepositoryBase<UserGroup>, IUserGroupRepository
{
    public UserGroupRepository(AppDbContext context) : base(context) { }


}
