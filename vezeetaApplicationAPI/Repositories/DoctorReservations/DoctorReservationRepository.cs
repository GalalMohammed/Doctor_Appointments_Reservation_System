using DAL.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vezeetaApplicationAPI.DataAccess;
using vezeetaApplicationAPI.Models;

namespace DAL.Repositories.DoctorReservations
{
    internal class DoctorReservationRepository : GenericRepository<DoctorReservation>, IDoctorReservationRepository
    {
        private readonly AppDbContext context;
        public DoctorReservationRepository(AppDbContext _context) : base(_context)
        {
            context = _context;
        }
        public override async Task<List<DoctorReservation>> GetAll(bool WithAsNoTracking = true)
        {
            if (WithAsNoTracking)
            {
                return await context.DoctorReservations.Include(x=>x.Doctor)
                    .Include(x=>x.Appointments).AsNoTracking().ToListAsync();
            }
            return await context.DoctorReservations.Include(x => x.Doctor)
                    .Include(x => x.Appointments).ToListAsync();
        }
        public override async Task<DoctorReservation> GetByID(int id, bool WithAsNoTracking = true)
        {
            var res = await context.DoctorReservations.Include(x => x.Doctor)
                    .Include(x => x.Appointments).Where(x=>x.ID== id).FirstOrDefaultAsync();
            if (WithAsNoTracking)
            {
                context.Entry(res).State = EntityState.Detached;
            }
            return res;
        }
    }
}
