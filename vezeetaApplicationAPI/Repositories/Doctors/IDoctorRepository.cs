using DAL.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using vezeetaApplicationAPI.Models;

namespace DAL.Repositories.Doctors
{
    public interface IDoctorRepository:IGenericRepository<Doctor>
    {
        public Task<List<Doctor>> GetFilteredByConditonPages(Expression<Func<Doctor, bool>> condition, int pageNum = 0, int pageSize = 10, bool WithAsNoTracking = true);

        public Task<int> GetFilteredByConditonCount(Expression<Func<Doctor, bool>> condition);

    }
}
