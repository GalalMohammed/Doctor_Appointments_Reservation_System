using DAL.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vezeetaApplicationAPI.DataAccess;
using vezeetaApplicationAPI.Models;

namespace DAL.Repositories.Appointments
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        private readonly AppDbContext context;
        public AppointmentRepository(AppDbContext _context) : base(_context)
        {
            context = _context;
        }
        public override async Task<List<Appointment>> GetAll(bool WithAsNoTracking = true)
        {
            if (WithAsNoTracking)
            {
                return await context.Appointments.Include(a => a.DoctorReservation).ThenInclude(a => a!.Doctor).ThenInclude(a => a!.Specialty)
                    .Include(a => a.Patient).AsNoTracking().ToListAsync();
            }
            return await context.Appointments.Include(a => a.DoctorReservation).ThenInclude(a => a!.Doctor).ThenInclude(a => a!.Specialty)
                    .Include(a => a.Patient).ToListAsync();
        }
        public override async Task<Appointment> GetByID(int id, bool WithAsNoTracking = true)
        {
            var res = await context.Appointments.Include(a => a.DoctorReservation).ThenInclude(a => a!.Doctor).ThenInclude(a => a!.Specialty)
                    .Include(a => a.Patient).Where(a => a.ID == id).FirstOrDefaultAsync();
            if (WithAsNoTracking)
            {
                context.Entry(res).State = EntityState.Detached;
            }
            return res;
        }
    }
}
