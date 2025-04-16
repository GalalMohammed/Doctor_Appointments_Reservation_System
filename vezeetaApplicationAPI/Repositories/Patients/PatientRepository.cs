using DAL.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using vezeetaApplicationAPI.DataAccess;
using vezeetaApplicationAPI.Models;

namespace DAL.Repositories.Patients
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        public PatientRepository(AppDbContext _context) : base(_context)
        {
        }
        public override async Task<List<Patient>> GetAll(bool WithAsNoTracking = true)
        {
            if (WithAsNoTracking)
            {
                return await context.Patients.Include(p => p.AppUser).Include(p => p.Appointments)
                    .AsNoTracking().ToListAsync();
            }
            return await context.Patients.Include(p => p.AppUser).Include(p => p.Appointments)
                    .ToListAsync();
        }
        public override async Task<Patient> GetByID(int id, bool WithAsNoTracking = true)
        {
            var res = await context.Patients.Include(p => p.AppUser)
                .Include(p => p.Appointments).ThenInclude(a => a.DoctorReservation).ThenInclude(d => d.Doctor).ThenInclude(d => d.Specialty).Where(p => p.ID == id).FirstOrDefaultAsync();
            if (WithAsNoTracking && res != null)
            {
                context.Entry(res).State = EntityState.Detached;
            }
            return res;
        }
    }
}
