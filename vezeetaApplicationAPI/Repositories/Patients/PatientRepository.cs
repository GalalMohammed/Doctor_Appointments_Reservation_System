using DAL.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vezeetaApplicationAPI.DataAccess;
using vezeetaApplicationAPI.Models;

namespace DAL.Repositories.Patients
{
    internal class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        private readonly AppDbContext context;
        public PatientRepository(AppDbContext _context) : base(_context)
        {
        }
        public override async Task<List<Patient>> GetAll(bool WithAsNoTracking = true)
        {
            if (WithAsNoTracking)
            {
                return await context.Patients.Include(p=>p.AppUser).Include(p=>p.Appointments)
                    .AsNoTracking().ToListAsync();
            }
            return await context.Patients.Include(p => p.AppUser).Include(p => p.Appointments)
                    .ToListAsync();
        }
        public override async Task<Patient> GetByID(int id, bool WithAsNoTracking = true)
        {
            var res = await context.Patients.Include(p => p.AppUser)
                .Include(p => p.Appointments).Where(p => p.ID == id).FirstOrDefaultAsync();
            if (WithAsNoTracking)
            {
                context.Entry(res).State = EntityState.Detached;
            }
            return res;
        }
    }
}
