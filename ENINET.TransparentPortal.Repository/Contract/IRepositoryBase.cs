using System.Linq.Expressions;

namespace ENINET.TransparentPortal.Repository.Contract;


public interface IRepositoryBase<T>
{
    IQueryable<T> FindAll(bool trackChanges);
    IQueryable<T> FindByCondition(Expression<Func<T, object>>? includes, Expression<Func<T, bool>> expression, bool trackChanges);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);

    bool Any(Expression<Func<T, bool>> expression);
}

