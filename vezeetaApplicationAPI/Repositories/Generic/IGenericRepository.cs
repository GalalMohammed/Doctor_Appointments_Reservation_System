using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Generic
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<List<T>> GetAll(bool WithAsNoTracking = true);
        public Task<T> GetByID(int id, bool WithAsNoTracking = true);
        public void Add(T entity);
        public void Update(T entity);
        public void Delete(T entity);


    }
}
