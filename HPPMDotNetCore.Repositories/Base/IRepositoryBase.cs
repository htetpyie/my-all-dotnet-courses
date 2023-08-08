using System;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;

namespace HPPMDotNetCore.Repositories.Base
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        int Create(T entity);
        int Update(T entity);
        int Delete(T entity);
        Task<int> CreateAsync(T entity);
    }
}