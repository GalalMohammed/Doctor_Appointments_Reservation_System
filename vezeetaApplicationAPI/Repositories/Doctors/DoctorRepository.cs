using DAL.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using vezeetaApplicationAPI.DataAccess;
using vezeetaApplicationAPI.Models;

namespace DAL.Repositories.Doctors
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        private AppDbContext context;
        public DoctorRepository(AppDbContext _context) : base(_context)
        {
            context = _context;
        }
        public override async Task<List<Doctor>> GetAll(bool WithAsNoTracking = true)
        {
            if (WithAsNoTracking)
            {
                return await context.Doctors.Include(d=>d.Specialty).Include(d=>d.AppUser)
                    .Include(d => d.DoctorReservations).Include(d => d.Reviews)
                    .AsNoTracking().ToListAsync();
            }
            return await context.Doctors.Include(d => d.Specialty).Include(d => d.AppUser)
                    .Include(d => d.DoctorReservations).Include(d => d.Reviews)
                    .ToListAsync();

        }
        public override async Task<Doctor> GetByID(int id, bool WithAsNoTracking = true)
        {
            var res = await context.Doctors.Include(d => d.Specialty).Include(d => d.AppUser)
                    .Include(d => d.DoctorReservations).Include(d => d.Reviews)
                    .Where(d=>d.ID == id).FirstOrDefaultAsync();
            if (WithAsNoTracking)
            {
                context.Entry(res).State = EntityState.Detached;
            }
            return res;
        }

        public async Task<List<Doctor>> GetFilteredByConditonPages(Expression<Func<Doctor, bool>> condition, int pageNum = 0,int pageSize = 10, bool WithAsNoTracking = true)
        {
            if (WithAsNoTracking)
            {
                var res = context.Doctors.AsNoTracking().Where(condition).Skip(pageNum * pageSize).Take(pageSize);
                return res.ToList();
            }
            return context.Doctors.Where(condition).Skip(pageNum * pageSize).Take(pageSize).ToList();

        }

        public async Task<int> GetFilteredByConditonCount(Expression<Func<Doctor, bool>> condition)
        {
            return context.Doctors.Where(condition).Count();
        }


    }
}
