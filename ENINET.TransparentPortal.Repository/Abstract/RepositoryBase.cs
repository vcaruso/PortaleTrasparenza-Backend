using ENINET.TransaprentPortal.Persistence;
using ENINET.TransparentPortal.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace ENINET.TransparentPortal.Repository.Abstract
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected AppDbContext AppDbContext { get; set; }
        public RepositoryBase(AppDbContext repositpryContext)
        {
            AppDbContext = repositpryContext;

        }
        public void Create(T entity) => AppDbContext.Set<T>().Add(entity);


        public void Delete(T entity) => AppDbContext.Set<T>().Remove(entity);


        public IQueryable<T> FindAll(bool trackChanges) => !trackChanges ? AppDbContext.Set<T>().AsNoTracking() : AppDbContext.Set<T>();


        public IQueryable<T> FindByCondition(Expression<Func<T, object>>? includes, Expression<Func<T, bool>> expression, bool trackChanges)
        {
            if (!trackChanges)
            {
                if (includes == null)
                {
                    return AppDbContext.Set<T>().Where(expression).AsNoTracking();
                }
                else
                {
                    return AppDbContext.Set<T>().Include(includes).Where(expression).AsNoTracking();
                }
            }
            else
            {
                if (includes == null)
                {
                    return AppDbContext.Set<T>().Where(expression);
                }
                else
                {
                    return AppDbContext.Set<T>().Include(includes).Where(expression);
                }

            }
        }


        public void Update(T entity) => AppDbContext.Set<T>().Update(entity);

        public bool Any(System.Linq.Expressions.Expression<Func<T, bool>> expression) => AppDbContext.Set<T>().Any(expression);

    }
}
