using System;
using System.Linq.Expressions;
using System.Linq;
using HPPMDotNetCore.DbService;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HPPMDotNetCore.Repositories.Base
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected EFDbContext _db { get; set; }
       
        public RepositoryBase(EFDbContext db)
        {
            _db = db;
        }
        
        public IQueryable<T> FindAll() => _db.Set<T>().AsNoTracking();
        
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            _db.Set<T>().Where(expression).AsNoTracking();
        
        public int Create(T entity)
        {
            _db.Set<T>().Add(entity);
            int result = _db.SaveChanges();
            return result;
        }
        
        public int Update(T entity)
        {
            _db.Set<T>().Update(entity);
            int result = _db.SaveChanges();
            return result ;
        }
       
        public int Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
            int result = _db.SaveChanges();
            return result;
        }

        public async Task<int> CreateAsync(T entity)
        {
            await _db.Set<T>().AddAsync(entity);
            int result = await _db.SaveChangesAsync();
            return result;
        }       
    } 
}
