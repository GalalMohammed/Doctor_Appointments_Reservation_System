using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading;
using vezeetaApplicationAPI.DataAccess;

namespace DAL.Repositories.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected AppDbContext context;
        public GenericRepository(AppDbContext _context)
        {
            context = _context;

        }
        public virtual async void Add(T entity)
        {
            context.Set<T>().Add(entity);
            context.SaveChanges();
        }

        public virtual async void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            context.SaveChanges();
        }

        public virtual async Task<List<T>> GetAll(bool WithAsNoTracking = true)
        {
            if (WithAsNoTracking)
            {
                return await context.Set<T>().AsNoTracking().ToListAsync();
            }
            return await context.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetByID(int id, bool WithAsNoTracking = true)
        {
            var res = await context.Set<T>().FindAsync(id);
            if (WithAsNoTracking)
            {
                context.Entry(res).State = EntityState.Detached;
            }
            return res;

        }

        public virtual async void Update(T entity)
        {
            context.Update(entity);
            context.SaveChanges();
        }
        public virtual async Task<List<T>> GetAllByConditon(Func<T, bool> condition, bool WithAsNoTracking = true)
        {
            if (WithAsNoTracking)
            {
                var res = context.Set<T>().AsNoTracking().Where(condition);
                return res.ToList();
            }
            return context.Set<T>().Where(condition).ToList();

        }
        public virtual async Task<List<T>> GetAllByConditonFiltterInDatabase(Expression<Func<T, bool>> condition, bool WithAsNoTracking = true)
        {
            var query = context.Set<T>().Where(condition);
            if (WithAsNoTracking)
                query = query.AsNoTracking();
            return await query.ToListAsync();

        }
    }
}
